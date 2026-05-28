using BOOKSTORE_API.Models.UserNamespace;
using Microsoft.EntityFrameworkCore;

namespace BOOKSTORE_API.ServicesNamespace.SqlDbNamespace;

public class SqlDbCtx : DbContext
{
  public SqlDbCtx(
      DbContextOptions<SqlDbCtx> options
  ) : base(options)
  {
  }

  public DbSet<User> Users => Set<User>();
}