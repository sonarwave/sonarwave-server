using Microsoft.EntityFrameworkCore;
using SonarWave.Core.Entities;
using SonarWave.Core.Enums;
using SonarWave.Core.EventArgs;
using SonarWave.Core.Interfaces;
using SonarWave.Core.Objects;
using SonarWave.Infrastructure.Data;

namespace SonarWave.Infrastructure.Services
{
    /// <summary>
    /// A service for room related operation.
    /// </summary>
    public class RoomService : IRoomService
    {
        private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;

        public event Func<UserJoinedRoomEventArgs, Task> OnUserJoinedRoomAsync = default!;

        public event Func<UserLeftRoomEventArgs, Task> OnUserLeftRoomAsync = default!;

        public RoomService(IDbContextFactory<DatabaseContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        #region JoinRoomAsync

        public async Task<Result<Room>> JoinRoomAsync(string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
                return Result<Room>.Failure(ErrorType.BadRequest, "ConnectionId invalid.");

            using DatabaseContext context = _dbContextFactory.CreateDbContext();

            User? user = await context.Users.FindAsync(connectionId);
            if (user == null)
                return Result<Room>.Failure(ErrorType.NotFound, "User not found.");

            Room? room = await context.Rooms
                .Include(opt => opt.Users)
                .FirstOrDefaultAsync(opt => opt.Users.Any(opt => opt.RemoteIpAddress == user.RemoteIpAddress));

            if (room == null)
            {
                room = new Room();
                user.DisplayName = 0;
                context.Rooms.Add(room);
            }
            else
            {
                user.DisplayName = room.Users.MaxBy(opt => opt.DisplayName)!.DisplayName + 1;
            }

            user.Room = room;
            await context.SaveChangesAsync();
            await OnUserJoinedAsync(new UserJoinedRoomEventArgs()
            {
                RoomId = room.Id,
                User = user,
            });

            return Result<Room>.Success(room);
        }

        #endregion JoinRoomAsync

        #region JoinRoomAsync

        public async Task<Result<Room>> JoinRoomAsync(string connectionId, string roomId)
        {
            if (string.IsNullOrEmpty(connectionId) || !Guid.TryParse(roomId, out _))
                return Result<Room>.Failure(ErrorType.BadRequest, "Invalid ids.");

            using DatabaseContext context = _dbContextFactory.CreateDbContext();

            Room? room = await context.Rooms
                .Include(opt => opt.Users)
                .FirstOrDefaultAsync(opt => opt.Id == roomId);

            if (room == null)
                return Result<Room>.Failure(ErrorType.NotFound, "Room not found.");

            User? user = await context.Users.FindAsync(connectionId);

            if (user == null)
                return Result<Room>.Failure(ErrorType.NotFound, "User not found.");

            user.DisplayName = room.Users.MaxBy(opt => opt.DisplayName)!.DisplayName + 1;
            user.Room = room;
            await context.SaveChangesAsync();
            await OnUserJoinedAsync(new UserJoinedRoomEventArgs()
            {
                RoomId = room.Id,
                User = user,
            });

            return Result<Room>.Success(room);
        }

        #endregion JoinRoomAsync

        #region LeaveRoomAsync

        public async Task<Result<bool>> LeaveRoomAsync(string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
                return Result<bool>.Failure(ErrorType.BadRequest, "ConnectionId invalid.");

            using DatabaseContext context = _dbContextFactory.CreateDbContext();

            User? user = await context.Users
                .Include(opt => opt.Room)
                .ThenInclude(opt => opt.Users)
                .FirstOrDefaultAsync(opt => opt.ConnectionId == connectionId);

            if (user == null)
                return Result<bool>.Failure(ErrorType.NotFound, "User not found.");

            if (string.IsNullOrEmpty(user.RoomId))
                return Result<bool>.Failure(ErrorType.BadRequest, "User is not in any room.");

            string roomId = user.RoomId;
            user.RoomId = null;

            if (user.Room.Users.Count <= 1)
                context.Rooms.Remove(user.Room);

            await context.SaveChangesAsync();
            await OnUserLeftAsync(new UserLeftRoomEventArgs()
            {
                ConnectionId = connectionId,
                RoomId = roomId
            });

            return Result<bool>.Success(true);
        }

        #endregion LeaveRoomAsync

        #region OnUserJoinedAsync

        /// <summary>
        /// Notifies all the listeners that a user has just joined a room.
        /// </summary>
        /// <param name="args">Information about the event.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// </returns>
        protected virtual async Task OnUserJoinedAsync(UserJoinedRoomEventArgs args)
        {
            if (OnUserJoinedRoomAsync != null)
                await OnUserJoinedRoomAsync.Invoke(args);
        }

        #endregion OnUserJoinedAsync

        #region OnUserLeftAsync

        /// <summary>
        /// Notifies all the listeners that a user has just left a room.
        /// </summary>
        /// <param name="args">Information about the event.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// </returns>
        protected virtual async Task OnUserLeftAsync(UserLeftRoomEventArgs args)
        {
            if (OnUserLeftRoomAsync != null)
                await OnUserLeftRoomAsync.Invoke(args);
        }

        #endregion OnUserLeftAsync
    }
}