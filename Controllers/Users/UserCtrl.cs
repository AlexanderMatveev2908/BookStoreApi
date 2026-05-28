using BOOKSTORE_API.Models.BooksNamespace;

namespace BOOKSTORE_API.Router.UsersCtrlNamespace;


public static class UserCtrl
{
  public static async Task PostBook(HttpContext ctx)
  {

    var book = await ctx.Request.ReadFromJsonAsync<Book>();
    Console.WriteLine(book);


    await ctx.Response.WriteAsJsonAsync(
       new
       {
         status = 400,
         message = "things went well",
         book
       });

    return;
  }
}