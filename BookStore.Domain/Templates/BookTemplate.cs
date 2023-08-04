using Microsoft.AspNetCore.Http;

namespace BookStore.Domain.Templates;

public class BookTemplate
{
    public string Title { get; set; }
    public string Description { get; set; }

    public long AuthorId { get; set; }

    public string? ImagePath { get; set; }

    public IFormFile? Image { get; set; }

    public List<long> Genres { get; set; }
}