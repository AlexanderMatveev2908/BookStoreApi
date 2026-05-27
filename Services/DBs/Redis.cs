using BOOKSTORE_API.EnvVarsNamespace;
using StackExchange.Redis;

namespace BOOKSTORE_API.ServicesNamespace.RedisNamespace;

public static class Redis
{

    public static IConnectionMultiplexer Connection { get; private set; } = null!;

    public static async Task Connect(WebApplication app)
    {
        string host = EnvVars.Get("REDIS_HOST");
        string portStr = EnvVars.Get("REDIS_PORT");
        int port = int.Parse(portStr);
        string password = EnvVars.Get("REDIS_PASSWORD");

        ConfigurationOptions options = new()
        {
            EndPoints =
            {
                { host, port }
            },

            User = "default",
            Password = password,

            Ssl = true,
            AbortOnConnectFail = false
        };

        Connection =
            await ConnectionMultiplexer.ConnectAsync(options);

        IDatabase db = Connection.GetDatabase();

        TimeSpan ping = await db.PingAsync();

        Console.WriteLine(
            $"💾 Redis ping: {ping.TotalMilliseconds} ms 💾"
        );
    }
}