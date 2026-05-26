using Controllers.BookControllersNamespace;

namespace BOOKSTORE_API.Router.BooksRouterNamespace;

public static class BooksRouter
{
  public static void Map(RouteGroupBuilder api)
  {
    api.MapGet("/books", () =>
    BookControllers.GetBooks());
  }
}