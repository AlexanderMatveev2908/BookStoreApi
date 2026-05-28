using BOOKSTORE_API.Models.BooksNamespace;
using BOOKSTORE_API.TypesNamespace;

namespace BOOKSTORE_API.Controllers.BookControllersNamespace;

public static class BookControllers
{

  public static string[] GetBooks()
  {
    return
    [
        "Book 1",
            "Book 2",
            "Book 3"
    ];
  }

  public static IResult PostBook(HttpContext ctx)
  {
    BookDto dto = (BookDto)ctx.Items["bookDto"]!;

    return Results.Json(new
    {
      status = 201,
      message = "Book created",
      book = dto
    }, statusCode: 201);
  }
}