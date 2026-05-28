using System.ComponentModel.DataAnnotations;
using BOOKSTORE_API.TypesNamespace;

namespace BOOKSTORE_API.Middleware.BooksMiddlewareNamespace;

public static class BooksMiddleware
{

  public static async Task<bool> CheckBook(HttpContext ctx)
  {
    BookDto? dto = await ctx.Request.ReadFromJsonAsync<BookDto>();

    if (dto is null)
    {
      ctx.Response.StatusCode = 400;
      await ctx.Response.WriteAsJsonAsync(new
      {
        status = 400,
        message = "Invalid JSON body"
      });

      return false;
    }

    List<ValidationResult> errors = new();
    ValidationContext validationContext = new(dto);

    bool isValid = Validator.TryValidateObject(
      dto,
      validationContext,
      errors,
      validateAllProperties: true
    );

    if (!isValid)
    {
      ctx.Response.StatusCode = 400;
      await ctx.Response.WriteAsJsonAsync(new
      {
        status = 400,
        message = "Invalid book data",
        errors = errors.Select(e => new
        {
          field = e.MemberNames.FirstOrDefault(),
          error = e.ErrorMessage
        })
      });

      return false;
    }

    ctx.Items["bookDto"] = dto;

    return true;
  }
}