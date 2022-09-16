namespace SonarWave.Core.Entities
{
    /// <summary>
    /// Represents a room.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Represents a unique identifier.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Represents a collection of users in this room.
        /// </summary>
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}