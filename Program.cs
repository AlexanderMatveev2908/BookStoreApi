using BOOKSTORE_API.EnvVarsNamespace;
using BOOKSTORE_API.ExtensionsNamespace.RateLimitExtNamespace;
using BOOKSTORE_API.MiddlewareNamespace;
using BOOKSTORE_API.RouterNamespace;
using BOOKSTORE_API.ServicesNamespace.RedisNamespace;
using DotNetEnv;

Env.Load();

var builder =
    WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

EnvVars.CheckEnvVars();

await Redis.Connect(app);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRateLimit();

MainMiddleware.UseMainMiddleware(app);

Router.MapApi(app);

app.Run();