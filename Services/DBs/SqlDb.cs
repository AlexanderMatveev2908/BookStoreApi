using BOOKSTORE_API.EnvVarsNamespace;

namespace BOOKSTORE_API.ServicesNamespace.SqlDbNamespace;

public static class SqlDb
{
  public static async Task Connect()
  {
    string host = EnvVars.Get("DB_HOST");
    string port = EnvVars.Get("DB_PORT");
    string database = EnvVars.Get("DB_DATABASE");
    string user = EnvVars.Get("DB_USER");
    string pwd = EnvVars.Get("DB_PWD");

    var connectionString =
        $"Host={host};Port={port};Database={database};Username={user};Password={pwd};SSL Mode=Require;Trust Server Certificate=true";

    await using var connection = new Npgsql.NpgsqlConnection(connectionString);

    try
    {
      await connection.OpenAsync();
      Console.WriteLine("💾 Connected to the database 💾");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Failed to connect to the database: {ex.Message}");
    }
  }
}