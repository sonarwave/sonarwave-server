using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SonarWave.Core.Entities;

namespace SonarWave.Infrastructure.Data.Configs
{
    /// <summary>
    /// Configuration for <see cref="Room"/> entity.
    /// </summary>
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(opt => opt.Id);

            builder.HasMany(opt => opt.Users)
                .WithOne(opt => opt.Room)
                .HasForeignKey(opt => opt.RoomId);
        }
    }
}