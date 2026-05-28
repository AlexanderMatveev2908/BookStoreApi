using BOOKSTORE_API.Models.BooksNamespace;

namespace BOOKSTORE_API.Models.UserNamespace;



public class User
{
  public int Id { get; set; }

  public string Name { get; set; } = "";

  public string Email { get; set; } = "";

  public List<Books> Books { get; set; } = new();
}