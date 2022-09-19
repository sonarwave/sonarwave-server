using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SonarWave.Application.Models;
using SonarWave.Core.Entities;
using SonarWave.Core.Enums;
using SonarWave.Core.EventArgs;
using SonarWave.Core.Extensions;
using SonarWave.Core.Interfaces;
using SonarWave.Core.Models.User;
using SonarWave.Core.Objects;

namespace SonarWave.Application.Hubs
{
    public class ConnectionHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public ConnectionHub(IUserService userService, IRoomService roomService, IMapper mapper)
        {
            _userService = userService;
            _roomService = roomService;
            _mapper = mapper;

            _roomService.OnUserJoinedRoomAsync += OnUserJoinedRoomAsync;
            _roomService.OnUserLeftRoomAsync += OnUserLeftRoomAsync;
        }

        #region OnConnectedAsync

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext == null)
                return;

            var request = new CreateUserRequest()
            {
                ConnectionId = Context.ConnectionId,
                RemoteIpAddress = httpContext.Request.Headers["remote-ip-address"].ToString(),
                PlatformType = httpContext.Request.Headers["platform-type"].ToString().ToEnum<PlatformType>()
            };

            var result = await _userService.AddUserAsync(request);

            if (result.Succeeded)
                await base.OnConnectedAsync();
        }

        #endregion OnConnectedAsync

        #region OnDisconnectedAsync

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _roomService.LeaveRoomAsync(Context.ConnectionId);
            await _userService.RemoveUserAsync(Context.ConnectionId);
            await OnDisconnectedAsync(exception);
        }

        #endregion OnDisconnectedAsync

        #region JoinRoomAsync

        public async Task<Response<RoomItem>> JoinRoomAsync(string roomId = "")
        {
            Result<Room> result;

            if (string.IsNullOrEmpty(roomId))
            {
                result = await _roomService.JoinRoomAsync(Context.ConnectionId);
            }
            else
            {
                result = await _roomService.JoinRoomAsync(Context.ConnectionId, roomId);
            }

            if (result.Succeeded)
            {
                return Response<RoomItem>.Ok(_mapper.Map<RoomItem>(result.Value));
            }

            return result.Fault.ErrorType switch
            {
                ErrorType.NotFound => Response<RoomItem>.NotFound(result.Fault.ErrorMessage),
                _ => Response<RoomItem>.BadRequest(result.Fault.ErrorMessage),
            };
        }

        #endregion JoinRoomAsync

        #region LeaveRoomAsync

        public async Task<Response<bool>> LeaveRoomAsync()
        {
            var result = await _roomService.LeaveRoomAsync(Context.ConnectionId);

            if (result.Succeeded)
            {
                return Response<bool>.NoContent(result.Value);
            }

            return Response<bool>.BadRequest(result.Fault.ErrorMessage);
        }

        #endregion LeaveRoomAsync

        #region OnUserJoinedRoomAsync

        private async Task OnUserJoinedRoomAsync(UserJoinedRoomEventArgs args)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, args.RoomId);
            await Clients.OthersInGroup(args.RoomId).SendAsync("OnUserJoinedRoom", _mapper.Map<UserItem>(args.User));
        }

        #endregion OnUserJoinedRoomAsync

        #region OnUserLeftRoomAsync

        private async Task OnUserLeftRoomAsync(UserLeftRoomEventArgs args)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, args.RoomId);
            await Clients.OthersInGroup(args.RoomId).SendAsync("OnUserLeftRoom", args.ConnectionId);
        }

        #endregion OnUserLeftRoomAsync
    }
}