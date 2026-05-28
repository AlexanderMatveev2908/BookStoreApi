using BOOKSTORE_API.Router.UsersCtrlNamespace;

namespace BOOKSTORE_API.Router.UsersRouterNamespace;


public static class UserRouter
{
  public static void Map(RouteGroupBuilder api)
  {
    api.MapPost("/users", async (HttpContext ctx) => await UserCtrl.PostBook(ctx)
    );
  }
}