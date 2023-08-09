using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Entity;

[Table("genre")]
public class Genre
{
    [Column("id")]
    public long Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
}