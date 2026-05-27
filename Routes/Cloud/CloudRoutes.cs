using BOOKSTORE_API.ControllersNamespace.CloudNamespace;
using Microsoft.AspNetCore.Mvc;

namespace BOOKSTORE_API.RoutesNamespace.CloudNamespace;

public static class CloudRoutes
{
  public static void Map(RouteGroupBuilder api)
  {
    api.MapPost("/cloud/upload", async (HttpContext ctx, IFormFile file, [FromForm] string title) => await
    CloudControllers.Upload(ctx, file, title)
    ).DisableAntiforgery();
  }
}