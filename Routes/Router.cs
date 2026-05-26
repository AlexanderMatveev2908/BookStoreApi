using BOOKSTORE_API.Router.BookRoutes;

namespace BOOKSTORE_API.Routes;

public static class Router
{
  public static void MapApi(WebApplication app)
  {
    RouteGroupBuilder api = app.MapGroup("/api/v1");

    api.MapGet("/weatherforecast", () =>
    new
    {
      Message = "Hello from API v1"
    });

    BooksRouter.Map(api);
  }
}