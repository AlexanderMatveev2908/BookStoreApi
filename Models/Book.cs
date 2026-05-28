using BOOKSTORE_API.Models.UserNamespace;

namespace BOOKSTORE_API.Models.BooksNamespace;


public class Book
{
  public int Id { get; set; }

  public string Title { get; set; } = "";

  public string Author { get; set; } = "";

  public decimal Price { get; set; }

  public int OwnerId { get; set; }

  public User Owner { get; set; } = null!;
}