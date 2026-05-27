using BOOKSTORE_API.EnvVarsNamespace;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

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

  public static async Task<ImageUploadResult> UploadSingle(IFormFile file)
  {
    using Stream stream =
     file.OpenReadStream();

    ImageUploadParams uploadParams =
          new()
          {
            File = new FileDescription(
                  file.FileName,
                  stream
              ),

            Folder = "cs__books"
          };

    ImageUploadResult result =
 await Connection.UploadAsync(
     uploadParams
 );

    return result;
  }
}