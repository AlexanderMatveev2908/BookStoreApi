using BOOKSTORE_API.Middleware;
using BOOKSTORE_API.RouterNamespace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();

MainMiddleware.UseMainMiddleware(app);

Router.MapApi(app);

app.Run();

