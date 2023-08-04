using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookStore.Domain.Entity;

public class Book
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public string? ImagePath { get; set; }

    // One to one relationship with author
    [ForeignKey("Author")] public long AuthorId { get; set; }

    [JsonIgnore] public Author Author { get; set; }

    // Many to many Relationship with genre
    public IList<Genre> Genres { get; set; }
}