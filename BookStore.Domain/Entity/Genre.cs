using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entity;

public class Genre
{
    public long Id { get; set; }
    public string Name { get; set; }
}