
using BOOKSTORE_API.Controllers.BookControllersNamespace;
using BOOKSTORE_API.Filters;
using BOOKSTORE_API.ServicesNamespace.SqlDbNamespace;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace BOOKSTORE_API.Router.BooksRouterNamespace;

public static class BooksRouter
{
  public static void Map(RouteGroupBuilder api)
  {
    api.MapGet("/books", () =>
    BookControllers.GetBooks());

    api.MapPost(
      "/books",
      async (HttpContext ctx, [FromServices] SqlDbCtx db) =>
      {
        return await BookControllers.PostBook(ctx, db);
      }
    )
    .AddEndpointFilter<BookValidationFilter>();
  }



}