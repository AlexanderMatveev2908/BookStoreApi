namespace BOOKSTORE_API.Middleware.BooksMiddlewareNamespace;

public static class BooksMiddleware
{
  public static async Task UseBooksMiddleware(HttpContext ctx)
  {
    Console.WriteLine("Books middleware hit");
  }
}