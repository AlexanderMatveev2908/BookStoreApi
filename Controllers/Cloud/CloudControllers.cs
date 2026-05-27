using BOOKSTORE_API.ServicesNamespace.CloudSvcNamespace;

namespace BOOKSTORE_API.ControllersNamespace.CloudNamespace;

public static class CloudControllers
{
  public static async Task Upload(HttpContext ctx, IFormFile file, string title)
  {

    var result = await CloudSvc.UploadSingle(file);
    Console.WriteLine(result.PublicId);
    Console.WriteLine(result.Url);

    await ctx.Response.WriteAsJsonAsync(
          new
          {
            status = 401,
            message = "things went well",
            title
          });

    return;
  }
}