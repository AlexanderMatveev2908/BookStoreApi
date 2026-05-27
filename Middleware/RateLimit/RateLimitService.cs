using BOOKSTORE_API.ServicesNamespace
.RedisNamespace;

using StackExchange.Redis;

namespace BOOKSTORE_API.ServicesNamespace
.RateLimitNamespace;

public static class RateLimitService
{
  private const int LIMIT = 10;

  private static readonly TimeSpan WINDOW =
      TimeSpan.FromHours(1);

  public static async Task<TimeSpan?>
  Limit(HttpContext ctx)
  {
    IDatabase db =
        Redis.Connection.GetDatabase();

    string ip =
        ctx.Connection
            .RemoteIpAddress?.ToString()
        ?? "unknown";

    string path =
        ctx.Request.Path;

    string method =
        ctx.Request.Method;

    string key =
        $"rl:{ip}:{path}:{method}";

    int requests =
        (int)await db.StringIncrementAsync(key);

    if (requests == 1)
    {
      await db.KeyExpireAsync(
          key,
          WINDOW
      );
    }

    int remaining =
        Math.Max(0, LIMIT - requests);

    ctx.Response.Headers["RateLimit-Limit"] =
        LIMIT.ToString();

    ctx.Response.Headers["RateLimit-Remaining"] =
        remaining.ToString();

    if (requests > LIMIT)
    {
      return await db.KeyTimeToLiveAsync(key);
    }

    return null;
  }
}