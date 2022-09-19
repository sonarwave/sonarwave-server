using SonarWave.Core.Enums;

namespace SonarWave.Application.Models
{
    /// <summary>
    /// Represents a user dto.
    /// </summary>
    public class UserItem
    {
        /// <summary>
        /// Represents the connection id of the connected user.
        /// </summary>
        public string ConnectionId { get; set; } = default!;

        /// <summary>
        /// Represents the display name of the user.
        /// </summary>
        public int DisplayName { get; set; }

        /// <summary>
        /// The sonarwave platform used to connect to the server.
        /// </summary>
        public PlatformType PlatformType { get; set; } = default!;

        /// <summary>
        /// Represents the id of the group that the user is in.
        /// </summary>
        public string? RoomId { get; set; }
    }
}