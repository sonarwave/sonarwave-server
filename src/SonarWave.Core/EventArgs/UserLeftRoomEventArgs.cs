namespace SonarWave.Core.EventArgs
{
    /// <summary>
    /// Information about when a user leaves a room.
    /// </summary>
    public struct UserLeftRoomEventArgs
    {
        /// <summary>
        /// Represents the id of the room.
        /// </summary>
        public string RoomId { get; set; } = default!;

        /// <summary>
        /// Represents the connectionId of the user.
        /// </summary>
        public string ConnectionId { get; set; } = default!;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UserLeftRoomEventArgs()
        {
        }
    }
}