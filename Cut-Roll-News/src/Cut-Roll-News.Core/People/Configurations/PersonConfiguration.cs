namespace Cut_Roll_News.Core.People.Configurations;

using Cut_Roll_News.Core.People.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("people")
            .HasKey(c => c.Id);

        builder.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()");
    }
}

