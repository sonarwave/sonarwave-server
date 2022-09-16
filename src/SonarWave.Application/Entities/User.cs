using SonarWave.Application.Enums;

namespace SonarWave.Application.Entities
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Represents the connection id of the connected user.
        /// </summary>
        public string ConnectionId { get; set; } = default!;

        /// <summary>
        /// Represents the remote ip address of the connected network.
        /// </summary>
        public string RemoteIpAddress { get; set; } = default!;

        /// <summary>
        /// The sonarwave platform used to connect to the server.
        /// </summary>
        public PlatformType PlatformType { get; set; } = default!;

        /// <summary>
        /// Represents the id of the group that the user is in.
        /// </summary>
        public string? RoomId { get; set; }

        // <summary>
        /// Represents the group that the user is in.
        /// </summary>
        public virtual Room? Room { get; set; }
    }
}