using Cut_Roll_News.Core.Users.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cut_Roll_News.Core.Users.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.HasKey(u => u.Id);

        builder
            .Property(u => u.Id)
            .ValueGeneratedNever();

        builder.Property(u => u.IsBanned)
            .IsRequired();

        builder.Property(u => u.IsMuted)
            .IsRequired();

        builder.Property(u => u.Email)
            .IsRequired();
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.UserName)
            .IsRequired();
        builder.HasIndex(u => u.UserName)
            .IsUnique();
            
        builder.HasMany(u => u.NewsArticles)
            .WithOne(n => n.Author)
            .HasForeignKey(n => n.AuthorId);
    }
}