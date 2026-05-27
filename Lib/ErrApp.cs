namespace BOOKSTORE_API.ErrAppNamespace;

public class ErrApp : Exception
{
  public int StatusCode { get; set; }

  public ErrApp(
      string message,
      int statusCode = 500
  ) : base(message)
  {
    StatusCode = statusCode;
  }
}