using SonarWave.Core.Enums;

namespace SonarWave.Core.Entities
{
    /// <summary>
    /// Represents a File.
    /// </summary>
    public class File
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
        public int Size { get; set; }

        /// <summary>
        /// Represents the id of the user that sent this file.
        /// </summary>
        public string SenderId { get; set; } = default!;

        /// <summary>
        /// Represents the user that sent this file.
        /// </summary>
        public virtual User Sender { get; set; } = default!;

        /// <summary>
        /// Represents the id of the user that should receive this file.
        /// </summary>
        public string RecipientId { get; set; } = default!;

        /// <summary>
        /// Represents the user that should receive this file.
        /// </summary>
        public virtual User Recipient { get; set; } = default!;
    }
}