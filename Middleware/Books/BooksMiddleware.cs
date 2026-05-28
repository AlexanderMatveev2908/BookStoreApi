using System.ComponentModel.DataAnnotations;
using BOOKSTORE_API.TypesNamespace;

namespace BOOKSTORE_API.Middleware.BooksMiddlewareNamespace;

public static class BooksMiddleware
{

  public static async Task<IResult?> CheckBook(HttpContext ctx)
  {
    if (!ctx.Request.HasJsonContentType())
    {
      return Results.Json(new
      {
        status = 415,
        message = "Content-Type must be application/json"
      }, statusCode: 415);
    }

    BookDto? dto = await ctx.Request.ReadFromJsonAsync<BookDto>();

    if (dto is null)
    {
      return Results.BadRequest(new
      {
        status = 400,
        message = "Invalid JSON body"
      });
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
      return Results.BadRequest(new
      {
        status = 400,
        message = "Invalid book data",
        errors = errors.Select(e => new
        {
          field = e.MemberNames.FirstOrDefault(),
          error = e.ErrorMessage
        })
      });
    }

    ctx.Items["bookDto"] = dto;

    return null;
  }
}