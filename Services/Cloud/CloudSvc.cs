using BOOKSTORE_API.EnvVarsNamespace;
using CloudinaryDotNet;

namespace BOOKSTORE_API.ServicesNamespace.CloudSvcNamespace;


public static class CloudSvc
{
  public static Cloudinary Connection
  {
    get;
    private set;
  } = null!;


  public static async Task Connect()
  {
    Account account = new(
        EnvVars.Get("CLOUD_NAME"),
        EnvVars.Get("CLOUD_API_KEY"),
        EnvVars.Get("CLOUD_API_SECRET")
    );

    Connection = new Cloudinary(account);

    var result = await Connection.PingAsync();

    Console.WriteLine(
        $"☁️ Cloud connected: {result.StatusCode} ☁️"
    );
  }
}