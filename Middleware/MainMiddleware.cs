using BOOKSTORE_API.Middleware.BooksMiddlewareNamespace;

namespace BOOKSTORE_API.Middleware;

public static class MainMiddleware
{
  private const string BASE_PATH = "/api/v1";
  private const string BOOKS_PATH = $"{BASE_PATH}/books";

  public static void UseMainMiddleware(this WebApplication app)
  {
    app.Use(async (ctx, next) =>
    {
      if (ctx.Request.Path.StartsWithSegments(BASE_PATH))
      {
        Console.WriteLine("Main middleware hit");

        if (ctx.Request.Path.StartsWithSegments(BOOKS_PATH))
          await BooksMiddleware.UseBooksMiddleware(ctx);
      }

      await next();
    });
  }
}