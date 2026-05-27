using BOOKSTORE_API.EnvVarsNamespace;
using BOOKSTORE_API.ExtensionsNamespace.RateLimitExtNamespace;
using BOOKSTORE_API.MiddlewareNamespace;
using BOOKSTORE_API.RouterNamespace;
using BOOKSTORE_API.ServicesNamespace.RedisNamespace;
using DotNetEnv;

Env.Load();

EnvVars.CheckEnvVars();

var builder =
    WebApplication.CreateBuilder(args);

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

var app = builder.Build();

await Redis.Connect(app);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRateLimit();

app.UseMainMiddleware();

Router.MapApi(app);

app.Run();