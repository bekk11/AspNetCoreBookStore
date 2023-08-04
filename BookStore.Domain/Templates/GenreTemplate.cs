using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Templates;

public class GenreTemplate
{
    [Required(ErrorMessage = "Enter the name!")]
    [MinLength(2, ErrorMessage = "Length of the name should be more than 2!")]
    public string Name { get; set; }
}