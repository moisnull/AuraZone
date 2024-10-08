using Microsoft.EntityFrameworkCore;
using AuraZoneAPI.DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuraZoneAPI.DataAccess.ModelsConfigurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) 
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(x => x.Username).HasColumnType("VARCHAR(200)").IsRequired();

            builder.Property(x => x.Email).HasColumnType("VARCHAR(200)").IsRequired();

            builder.Property(x => x.PasswordHashed).HasColumnType("VARCHAR(200)").IsRequired();

            builder.HasMany(x => x.Videos).WithOne(x => x.User).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Comments).WithOne(x => x.User).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
