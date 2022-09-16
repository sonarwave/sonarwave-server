using SonarWave.Core.Enums;

namespace SonarWave.Core.Models.User
{
    /// <summary>
    /// A model for adding a user.
    /// </summary>
    public class CreateUserRequest
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
    }
}