using System.Text.Json.Serialization;

namespace BookStore.Domain.Templates;

public class AuthorTemplate
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public int Age { get; set; }
}