using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookStore.Domain.Entity;

[Table("book")]
public class Book
{
    [Column("id")]
    public long Id { get; set; }

    [Column("title")]
    public string Title { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("createdAt")]
    public DateTime CreatedAt { get; set; }

    [Column("imagePath")]
    public string? ImagePath { get; set; }

    // One to one relationship with author
    [ForeignKey("Author")] public long AuthorId { get; set; }

    [JsonIgnore] public Author Author { get; set; }

    // Many to many Relationship with genre
    public IList<Genre> Genres { get; set; }
}