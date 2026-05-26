namespace BOOKSTORE_API.Router.BookRoutes;

public static class BooksRouter
{
  public static void Map(RouteGroupBuilder api)
  {
    api.MapGet("/books", () =>
    {
      return new string[]
          {
                "Book 1",
                "Book 2",
                "Book 3"
        };
    });
  }
}