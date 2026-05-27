using BOOKSTORE_API.ErrAppNamespace;

namespace BOOKSTORE_API.EnvVarsNamespace;

public static class EnvVars
{
  private static readonly string[] KEYS =
  {
    "REDIS_HOST",
    "REDIS_PORT",
    "REDIS_PASSWORD",
    "FRONTEND_URL",
    "CLOUD_NAME",
    "CLOUD_API_KEY",
    "CLOUD_API_SECRET",
    "CLOUD_URL"
  };

  public static string Get(string key)
  {
    string? value = Environment.GetEnvironmentVariable(key);

    if (value is null)
    {
      throw new ErrApp(
          $"Environment variable '{key}' is not set"

      );
    }

    return value;
  }

  public static void CheckEnvVars()
  {
    foreach (string key in KEYS)
    {
      Get(key);
    }
  }
}