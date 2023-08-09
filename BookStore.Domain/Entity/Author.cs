using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookStore.Domain.Templates;

namespace BookStore.Domain.Entity;

[Table("author")]
public class Author
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    
    [Column("firstname")]
    public string Firstname { get; set; }
    
    [Column("lastname")]
    public string Lastname { get; set; }
    
    [Column("age")]
    public int Age { get; set; }
}