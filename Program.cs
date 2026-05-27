using BOOKSTORE_API.EnvVarsNamespace;
using BOOKSTORE_API.ExtensionsNamespace.RateLimitExtNamespace;
using BOOKSTORE_API.MiddlewareNamespace;
using BOOKSTORE_API.RouterNamespace;
using BOOKSTORE_API.ServicesNamespace.RedisNamespace;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

EnvVars.CheckEnvVars();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();

RateLimitExt.UseRateLimit(app);

MainMiddleware.UseMainMiddleware(app);

Router.MapApi(app);

await Redis.Connect(app);

app.Run();

