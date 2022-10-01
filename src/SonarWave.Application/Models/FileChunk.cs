namespace SonarWave.Application.Models
{
    /// <summary>
    /// Represents a chunk of the file.
    /// </summary>
    public class FileChunk
    {
        /// <summary>
        /// Represents the id of the file.
        /// </summary>
        public string FileId { get; set; } = default!;

        /// <summary>
        /// Represents a chunk of the file.
        /// </summary>
        public byte[] Chunk { get; set; } = Array.Empty<byte>();
    }
}