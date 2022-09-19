namespace SonarWave.Application.Models
{
    /// <summary>
    /// Represents a room dto.
    /// </summary>
    public class RoomItem
    {
        /// <summary>
        /// Represents a unique identifier.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Represents a collection of users in this room.
        /// </summary>
        public virtual ICollection<UserItem> Users { get; set; } = new List<UserItem>();
    }
}