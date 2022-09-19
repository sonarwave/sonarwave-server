using SonarWave.Core.Entities;
using SonarWave.Core.Enums;
using SonarWave.Core.EventArgs;
using SonarWave.Core.Objects;

namespace SonarWave.Core.Interfaces
{
    /// <summary>
    /// An interface for room related operations.
    /// </summary>
    public interface IRoomService
    {
        /// <summary>
        /// An event for when a user joins a <see cref="Room"/>.
        /// </summary>
        public event Func<UserJoinedRoomEventArgs, Task> OnUserJoinedRoomAsync;

        /// <summary>
        /// An event for when a user leaves a <see cref="Room"/>.
        /// </summary>
        public event Func<UserLeftRoomEventArgs, Task> OnUserLeftRoomAsync;

        /// <summary>
        /// Used for joining a <see cref="Room"/>.
        /// </summary>
        /// <param name="connectionId">Represents the connectionId of the <see cref="User"/>.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// A <see cref="Result{Room}"/>, containing detailes of operation.
        /// </returns>
        /// <remarks>
        /// May produce the following errors.
        /// <list type="bullet">
        /// <item><see cref="ErrorType.BadRequest"/></item>
        /// </list>
        /// </remarks>
        public Task<Result<Room>> JoinRoomAsync(string connectionId);

        /// <summary>
        /// Used for joining a <see cref="Room"/>.
        /// </summary>
        /// <param name="connectionId">Represents the connectionId of the <see cref="User"/>.</param>
        /// <param name="roomId">Represents the id of a <see cref="Room"/>.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// A <see cref="Result{Room}"/>, containing detailes of operation.
        /// </returns>
        /// <remarks>
        /// May produce the following errors.
        /// <list type="bullet">
        /// <item><see cref="ErrorType.BadRequest"/></item>
        /// <item><see cref="ErrorType.NotFound"/></item>
        /// </list>
        /// </remarks>
        public Task<Result<Room>> JoinRoomAsync(string connectionId, string roomId);

        /// <summary>
        /// Used for leaving a room.
        /// </summary>
        /// <param name="connectionId">Represents the connectionId of the <see cref="User"/>.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// A <see cref="Result{bool}"/>, containing detailes of operation.
        /// </returns>
        /// <remarks>
        /// May produce the following errors.
        /// <list type="bullet">
        /// <item><see cref="ErrorType.BadRequest"/></item>
        /// </list>
        /// </remarks>
        public Task<Result<bool>> LeaveRoomAsync(string connectionId);
    }
}