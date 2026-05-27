using BOOKSTORE_API.ServicesNamespace.RedisNamespace;
using StackExchange.Redis;

namespace BOOKSTORE_API.Middleware;

public sealed class RateLimitMiddleware
{
  private readonly RequestDelegate _next;

  public RateLimitMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext ctx)
  {
    IDatabase db =
        Redis.Connection.GetDatabase();

    string ip =
        ctx.Connection.RemoteIpAddress?.ToString()
        ?? "unknown";

    string key =
        $"rate-limit:{ip}";

    int requests =
        (int)await db.StringIncrementAsync(key);

    if (requests == 1)
    {
      await db.KeyExpireAsync(
          key,
          TimeSpan.FromHours(1)
      );
    }

    if (requests > 10)
    {
      TimeSpan? ttl =
          await db.KeyTimeToLiveAsync(key);

      ctx.Response.StatusCode = 429;

      await ctx.Response.WriteAsync(
          $"Rate limit exceeded. Try again in {ttl?.Minutes} minutes."
      );

      return;
    }

    await _next(ctx);
  }
}