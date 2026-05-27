using BOOKSTORE_API.ControllersNamespace.CloudNamespace;

namespace BOOKSTORE_API.RoutesNamespace.CloudNamespace;

public static class CloudRoutes
{
  public static void Map(RouteGroupBuilder api)
  {
    api.MapPost("/cloud/upload", async (HttpContext ctx, IFormFile file) => await
    CloudControllers.Upload(ctx, file)
    ).DisableAntiforgery();
  }
}