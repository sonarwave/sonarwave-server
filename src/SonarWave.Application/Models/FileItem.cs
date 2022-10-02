using SonarWave.Core.Enums;

namespace SonarWave.Application.Models
{
    /// <summary>
    /// Represents a file dto.
    /// </summary>
    public class FileItem
    {
        /// <summary>
        /// Represents a unique identifier.
        /// </summary>
        public string Id { get; set; } = default!;

        /// <summary>
        /// Represents the name of the file.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Represents the path of the file.
        /// </summary>
        public string Path { get; set; } = default!;

        /// <summary>
        /// Whether or not the recipient has agreed on receiving the file.
        /// </summary>
        public TransferAcceptance Acceptance { get; set; }

        /// <summary>
        /// Represents the extensions of the file.
        /// </summary>
        public string Extension { get; set; } = default!;

        /// <summary>
        /// Represents the size of the file in Megabytes.
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// Represents the id of the user that sent this file.
        /// </summary>
        public string SenderId { get; set; } = default!;

        /// <summary>
        /// Represents the id of the user that should receive this file.
        /// </summary>
        public string RecipientId { get; set; } = default!;
    }
}