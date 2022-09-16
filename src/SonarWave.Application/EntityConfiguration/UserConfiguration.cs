using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SonarWave.Application.Entities;

namespace SonarWave.Application.EntityConfiguration
{
    /// <summary>
    /// Configuration for <see cref="User"/> entity.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(opt => opt.ConnectionId);

            builder.HasOne(opt => opt.Room)
                .WithMany(opt => opt.Users)
                .HasForeignKey(opt => opt.RoomId);
        }
    }
}