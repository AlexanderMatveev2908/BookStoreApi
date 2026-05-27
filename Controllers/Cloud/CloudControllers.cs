using BOOKSTORE_API.ServicesNamespace.CloudSvcNamespace;

namespace BOOKSTORE_API.ControllersNamespace.CloudNamespace;

public static class CloudControllers
{
  public static async Task Upload(HttpContext ctx, IFormFile file, string title)
  {

    var result = await CloudSvc.UploadSingle(file);

    await ctx.Response.WriteAsJsonAsync(
          new
          {
            status = 401,
            message = "things went well",
            title,
            result.Url,
            result.PublicId
          });

    return;
  }

  public static async Task Delete(HttpContext ctx, string publicId, string resourceType)
  {
    await CloudSvc.Delete(publicId, resourceType);

    await ctx.Response.WriteAsJsonAsync(
          new
          {
            status = 400,
            message = "things went well",
          });

    return;
  }
}