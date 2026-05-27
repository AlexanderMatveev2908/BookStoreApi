using BOOKSTORE_API.Middleware;

namespace BOOKSTORE_API.ExtensionsNamespace.RateLimitExtNamespace;


public static class RateLimitExt
{
  public static void UseRateLimit(this WebApplication app)
  {
    app.UseWhen(
        ctx => ctx.Request.Path.StartsWithSegments("/api/v1/books"),
        appBuilder =>
        {
          appBuilder.UseMiddleware<RateLimitMiddleware>();
        }
    );
  }
}