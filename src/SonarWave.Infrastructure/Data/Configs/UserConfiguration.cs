using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SonarWave.Core.Entities;

namespace SonarWave.Infrastructure.Data.Configs
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
                .HasForeignKey(opt => opt.RoomId)
                .IsRequired(false);

            builder.HasMany(opt => opt.FilesSent)
                .WithOne(opt => opt.Sender)
                .HasForeignKey(opt => opt.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(opt => opt.FilesReceived)
                .WithOne(opt => opt.Recipient)
                .HasForeignKey(opt => opt.RecipientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}