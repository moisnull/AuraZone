using AuraZoneAPI.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraZoneAPI.DataAccess.ModelsConfigurations
{
    public class VideoConfig : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.ToTable("Video");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(x => x.Name).HasColumnType("VARCHAR(200)").IsRequired();

            builder.Property(x => x.Url).HasColumnType("VARCHAR(200)").IsRequired();

            builder.Property(x => x.Category).HasColumnType("VARCHAR(200)").IsRequired();

            builder.Property(x => x.ThumbnailUrl).HasColumnType("VARCHAR(200)");

            builder.Property(x => x.Description).HasColumnType("TEXT");

            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired();

            // No cascading delete from User to Video
            builder.HasOne(x => x.User)
                .WithMany(x => x.Videos)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete from User to Video
        }
    }
}
