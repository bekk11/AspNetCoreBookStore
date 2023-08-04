using BookStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Domain.Configuration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(book => book.Id);
        builder.HasIndex(book => book.Title).IsUnique();

        builder.Property(book => book.Id).ValueGeneratedOnAdd();
        builder.Property(book => book.Title).HasMaxLength(150);
        builder.Property(book => book.Description).HasMaxLength(500);

        builder
            .HasMany(x => x.Genres)
            .WithMany("Books");
    }
}