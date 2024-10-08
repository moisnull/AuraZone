using AuraZoneAPI.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuraZoneAPI.DataAccess.ModelsConfigurations
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(x => x.Content)
                   .HasColumnType("TEXT")
                   .IsRequired();

            builder.Property(x => x.CreatedDate)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            // Foreign key to Video
            builder.HasOne(x => x.Video)
                   .WithMany(x => x.Comments)
                   .HasForeignKey(x => x.VideoId)
                   .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete from Video to Comments

            // Foreign key to User
            builder.HasOne(x => x.User)
                   .WithMany(x => x.Comments)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete from User to Comments
        }
    }
}
