using BOOKSTORE_API.EnvVarsNamespace;
using BOOKSTORE_API.ServicesNamespace.CloudSvcNamespace;
using BOOKSTORE_API.ServicesNamespace.RedisNamespace;
using BOOKSTORE_API.ServicesNamespace.SqlDbNamespace;
using BOOKSTORE_API.SettingsAppNamespace;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

Env.Load();

EnvVars.CheckEnvVars();

var builder =
    WebApplication.CreateBuilder(args);

SettingsApp.ConfigureBuilder(builder);

var app = builder.Build();

await CloudSvc.Connect();
await Redis.Connect(app);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

await SettingsApp.ConfigureApp(app);

app.Run();