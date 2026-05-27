using BOOKSTORE_API.MiddlewareNamespace.RateLimitNamespace;

namespace BOOKSTORE_API.Middleware;

public sealed class RateLimitMiddleware
{
  private readonly RequestDelegate _next;

  public RateLimitMiddleware(
      RequestDelegate next
  )
  {
    _next = next;
  }

  public async Task InvokeAsync(
      HttpContext ctx
  )
  {
    TimeSpan? ttl =
        await RateLimitService.Limit(ctx);

    if (ttl is not null)
    {
      ctx.Response.StatusCode = 429;

      await ctx.Response.WriteAsJsonAsync(
      new
      {
        status = 429,
        message =
              $"Rate limit exceeded. Try again in {ttl.Value.TotalMinutes:F0} minutes."
      }
  );
      return;
    }

    await _next(ctx);
  }
}