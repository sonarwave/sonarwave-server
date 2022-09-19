using SonarWave.Core.Entities;

namespace SonarWave.Core.EventArgs
{
    /// <summary>
    /// Information about when a user joins a room.
    /// </summary>
    public struct UserJoinedRoomEventArgs
    {
        /// <summary>
        /// Represents the id of the room.
        /// </summary>
        public string RoomId { get; set; } = default!;

        /// <summary>
        /// Represents the user that just joined.
        /// </summary>
        public User User { get; set; } = default!;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UserJoinedRoomEventArgs()
        {
        }
    }
}