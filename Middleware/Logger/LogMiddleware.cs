using System.Text;
using System.Text.Json;

namespace BOOKSTORE_API.MiddlewareNamespace.LogMiddlewareNamespace;

public static class LogMiddleware

{
  private static async Task<string> ReadBody(HttpRequest? req)
  {
    string body = "";


    if (req?.ContentLength > 0)
    {
      using (var reader = new StreamReader(req.Body, Encoding.UTF8, leaveOpen: true))
      {
        body = await reader.ReadToEndAsync();
        req.Body.Position = 0;
      }
    }

    return body;
  }

  private static async Task<object> BuildObj(HttpContext ctx)
  {
    var req = ctx.Request;

    var body = await ReadBody(req);

    var logObj = new
    {
      Timestamp = DateTime.UtcNow,
      Method = req?.Method,
      Path = req?.Path.ToString(),
      Query = req?.Query.ToDictionary(
        q => q.Key,
        q => q.Value.ToString()
    ),
      RouteParams = ctx.Request.RouteValues,
      Body = body
    };

    return logObj;
  }

  private static async Task Log(string json)
  {
    string cwd = Directory.GetCurrentDirectory();
    string logDirectory =
        Path.Combine(cwd, ".logger");
    string logFilePath =
        Path.Combine(logDirectory, "requests.json");

    if (!Directory.Exists(logDirectory))
      Directory.CreateDirectory(logDirectory);

    await File.AppendAllTextAsync(
        logFilePath,
        json + "," + Environment.NewLine
    );
  }

  public static async Task LogRequest(HttpContext ctx)
  {
    var req = ctx.Request;
    req.EnableBuffering();

    var logObj = await BuildObj(ctx);

    string json = JsonSerializer.Serialize(
              logObj,
              new JsonSerializerOptions
              {
                WriteIndented = true
              });

    await Log(json);

  }
}