using BOOKSTORE_API.Router.BooksRouterNamespace;
using BOOKSTORE_API.RoutesNamespace.CloudNamespace;

namespace BOOKSTORE_API.RouterNamespace;

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
    CloudRoutes.Map(api);
  }
}