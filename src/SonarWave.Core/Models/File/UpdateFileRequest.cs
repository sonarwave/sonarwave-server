using SonarWave.Core.Enums;

namespace SonarWave.Core.Models.File
{
    /// <summary>
    /// A model for updating a file.
    /// </summary>
    public class UpdateFileRequest
    {
        /// <summary>
        /// Represents a unique identifier.
        /// </summary>
        public string Id { get; set; } = default!;

        /// <summary>
        /// Whether or not the recipient has agreed on receiving the file.
        /// </summary>
        public TransferAcceptance Acceptance { get; set; }
    }
}