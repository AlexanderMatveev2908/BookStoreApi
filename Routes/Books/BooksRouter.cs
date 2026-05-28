
using BOOKSTORE_API.Controllers.BookControllersNamespace;
using BOOKSTORE_API.Filters;

namespace BOOKSTORE_API.Router.BooksRouterNamespace;

public static class BooksRouter
{
  public static void Map(RouteGroupBuilder api)
  {
    api.MapGet("/books", () =>
    BookControllers.GetBooks());

    api.MapPost(
      "/books",
      async (HttpContext ctx) =>
      {
        return BookControllers.PostBook(ctx);
      }
    )
    .AddEndpointFilter<BookValidationFilter>();
  }



}