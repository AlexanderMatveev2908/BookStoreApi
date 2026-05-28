using BOOKSTORE_API.Middleware.BooksMiddlewareNamespace;

namespace BOOKSTORE_API.Filters;

public class BookValidationFilter : IEndpointFilter
{
  public async ValueTask<object?> InvokeAsync(
    EndpointFilterInvocationContext context,
    EndpointFilterDelegate next
  )
  {
    IResult? errorResult =
     await BooksMiddleware.CheckBook(context.HttpContext);

    if (errorResult is not null)
      return errorResult;

    return await next(context);
  }
}