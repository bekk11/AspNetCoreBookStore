using BookStore.Domain.Templates;

namespace BookStore.Domain.Entity;

public class Author
{
    public long Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public int Age { get; set; }
}