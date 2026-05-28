using BOOKSTORE_API.Models.BooksNamespace;
using BOOKSTORE_API.Models.UserNamespace;
using BOOKSTORE_API.ServicesNamespace.SqlDbNamespace;
using BOOKSTORE_API.TypesNamespace;
using Dapper;
using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace BOOKSTORE_API.Controllers.BookControllersNamespace;

public static class BookControllers
{

  public static string[] GetBooks()
  {
    return
    [
        "Book 1",
            "Book 2",
            "Book 3"
    ];
  }

  public static async Task<IResult> PostBook(HttpContext ctx, SqlDbCtx db)
  {
    BookDto dto = (BookDto)ctx.Items["bookDto"]!;


    NpgsqlConnection conn =
        (NpgsqlConnection)db.Database.GetDbConnection();

    await conn.OpenAsync();

    await using NpgsqlTransaction trx =
       await conn.BeginTransactionAsync();

    try
    {
      User createdUser = await conn.QuerySingleAsync<User>(
          """
            INSERT INTO "Users" ("Name", "Email")
            VALUES (@Name, @Email)
            RETURNING *;
            """,
          new
          {
            Name = $"user_{Guid.NewGuid():N}",
            Email = $"user_{Guid.NewGuid():N}@example.com"
          },
          trx
      );


      Books createdBook = await conn.QuerySingleAsync<Books>(
          """
            INSERT INTO "Books" ("Title", "Author", "Price", "OwnerId")
            VALUES (@Title, @Author, @Price, @OwnerId)
            RETURNING *;
            """,
          new
          {
            Title = dto.Title,
            Author = dto.Author,
            Price = dto.Price,
            OwnerId = createdUser.Id
          },
          trx
      );

      await trx.CommitAsync();



      return Results.Json(new
      {
        status = 201,
        message = "Book created",
        book = createdBook,
        user = createdUser
      }, statusCode: 201);
    }
    catch
    {

      await trx.RollbackAsync();
      throw;
    }

  }
}