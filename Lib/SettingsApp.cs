using BOOKSTORE_API.EnvVarsNamespace;
using BOOKSTORE_API.ExtensionsNamespace.RateLimitExtNamespace;
using BOOKSTORE_API.MiddlewareNamespace;
using BOOKSTORE_API.ServicesNamespace.SqlDbNamespace;
using Microsoft.EntityFrameworkCore;

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

    builder.Services.AddDbContext<SqlDbCtx>(options =>
{
  options.UseNpgsql(EnvVars.Get("DB_URL"));
});

  }

  public static async Task CheckDb(WebApplication app)
  {
    try
    {
      using var scope =
          app.Services.CreateScope();

      SqlDbCtx db =
          scope.ServiceProvider
              .GetRequiredService<SqlDbCtx>();

      bool canConnect =
          await db.Database.CanConnectAsync();

      Console.WriteLine(
          canConnect
              ? "💾 Database connected 💾"
              : "❌ Database failed ❌"
      );
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  public static async Task ConfigureApp(WebApplication app)
  {

    await CheckDb(app);

    app.UseHttpsRedirection();

    app.UseCors("Frontend");

    app.UseRateLimit();

    app.UseMainMiddleware();

    BOOKSTORE_API.RouterNamespace.Router.MapApi(app);
  }
}