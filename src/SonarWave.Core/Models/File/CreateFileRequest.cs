namespace SonarWave.Core.Models.File
{
    /// <summary>
    /// A model for adding a file.
    /// </summary>
    public class CreateFileRequest
    {
        /// <summary>
        /// Represents the name of the file.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Represents the extensions of the file.
        /// </summary>
        public string Extension { get; set; } = default!;

        /// <summary>
        /// Represents the size of the file in Megabytes.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Represents the id of the recipient.
        /// </summary>
        public string RecipientId { get; set; } = default!;
    }
}