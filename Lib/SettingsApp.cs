using BOOKSTORE_API.EnvVarsNamespace;
using BOOKSTORE_API.ExtensionsNamespace.RateLimitExtNamespace;
using BOOKSTORE_API.MiddlewareNamespace;

namespace BOOKSTORE_API.SettingsAppNamespace;

public static class SettingsApp
{
  public static void ConfigureBuilder(WebApplicationBuilder builder)
  {
    builder.Services.AddOpenApi();

    builder.Services.AddCors(options =>
    {
      options.AddPolicy(
      "Frontend",
      policy =>
      {
        policy
        .WithOrigins(
            EnvVars.Get("FRONTEND_URL")
        )
        .AllowAnyMethod()
        .AllowAnyHeader();
      }
  );
    });


    builder.WebHost.ConfigureKestrel(options =>
    {
      options.Limits.MaxRequestBodySize =
      1024 * 1024 * 500;
    });

  }

  public static void ConfigureApp(WebApplication app)
  {

    app.UseHttpsRedirection();

    app.UseCors("Frontend");

    app.UseRateLimit();

    app.UseMainMiddleware();

    BOOKSTORE_API.RouterNamespace.Router.MapApi(app);
  }
}