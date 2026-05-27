namespace BOOKSTORE_API.ControllersNamespace.CloudNamespace;

public static class CloudControllers
{
  public static async Task Upload(HttpContext ctx, IFormFile file, string title)
  {

    Console.WriteLine(file);

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