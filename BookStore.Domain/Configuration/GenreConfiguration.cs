using BookStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Domain.Configuration;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(genre => genre.Id);
        builder.HasIndex(genre => genre.Name).IsUnique();


        builder.Property(genre => genre.Id).ValueGeneratedOnAdd();
        builder.Property(genre => genre.Name).HasMaxLength(100).IsRequired();
    }
}