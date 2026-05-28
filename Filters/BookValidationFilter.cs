using BOOKSTORE_API.Middleware.BooksMiddlewareNamespace;

namespace BOOKSTORE_API.Filters;

public class BookValidationFilter : IEndpointFilter
{
  public async ValueTask<object?> InvokeAsync(
    EndpointFilterInvocationContext context,
    EndpointFilterDelegate next
  )
  {
    HttpContext ctx = context.HttpContext;

    bool isValid = await BooksMiddleware.CheckBook(ctx);

    if (!isValid)
      return null;

    return await next(context);
  }
}