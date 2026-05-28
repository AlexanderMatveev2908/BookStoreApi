using System.ComponentModel.DataAnnotations;

namespace BOOKSTORE_API.TypesNamespace;


public class BookDto
{
  [Required]
  [MinLength(3)]
  public string Title { get; set; } = "";

  [Required]
  [MinLength(3)]
  public string Author { get; set; } = "";

  [Range(0.01, 2000.00)]
  public decimal Price { get; set; }

  [Required]
  public int OwnerId { get; set; }

}