namespace Cut_Roll_News.Core.Movies.Configurations;

using Cut_Roll_News.Core.Movies.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("movies")
            .HasKey(c => c.Id);

        builder.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()");
    }
}

