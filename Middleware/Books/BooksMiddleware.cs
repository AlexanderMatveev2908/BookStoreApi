namespace BOOKSTORE_API.Middleware.BooksMiddlewareNamespace;

public static class BooksMiddleware
{
  public static void UseBooksMiddleware(HttpContext ctx)
  {
    Console.WriteLine("Books middleware hit");
  }
}