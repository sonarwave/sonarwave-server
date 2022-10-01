using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = SonarWave.Core.Entities.File;

namespace SonarWave.Infrastructure.Data.Configs
{
    /// <summary>
    /// Configuration for <see cref="File"/> entity.
    /// </summary>
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.HasKey(opt => opt.Id);

            builder.HasOne(opt => opt.Sender)
                .WithMany(opt => opt.FilesSent)
                .HasForeignKey(opt => opt.SenderId);

            builder.HasOne(opt => opt.Recipient)
                .WithMany(opt => opt.FilesReceived)
                .HasForeignKey(opt => opt.RecipientId);
        }
    }
}