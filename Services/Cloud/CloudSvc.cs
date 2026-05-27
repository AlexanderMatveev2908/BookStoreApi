using BOOKSTORE_API.EnvVarsNamespace;
using BOOKSTORE_API.FilesLibNamespace;
using BOOKSTORE_API.TypesNamespace;
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

  public static async Task<UploadResultCloud> UploadImage(IFormFile file)
  {
    using Stream stream =
     file.OpenReadStream();

    ImageUploadParams uploadParams =
          new()
          {
            File = new FileDescription(
                  FilesLib.MakeNewFilename(file),
                  stream
              ),

            Folder = "cs__books"
          };

    ImageUploadResult result =
 await Connection.UploadAsync(
     uploadParams
 );

    return new UploadResultCloud
    {
      PublicId = result.PublicId,
      Url = result.Url.ToString()
    };
  }

  public static async Task<UploadResultCloud> UploadVideo(IFormFile file)
  {
    var cwd = Directory.GetCurrentDirectory();

    var fileName = FilesLib.MakeNewFilename(file);
    var tempPath = Path.Combine(
        cwd,
        "temp",
      fileName
    );

    Directory.CreateDirectory(Path.GetDirectoryName(tempPath)!);

    using (var videoStream = new FileStream(tempPath, FileMode.Create))
    {
      await file.CopyToAsync(videoStream);
    }

    VideoUploadParams uploadParamsVideo = new()
    {
      File = new FileDescription(tempPath),
      Folder = "cs__books"
    };

    VideoUploadResult resultVideo =
        await Connection.UploadAsync(uploadParamsVideo);

    File.Delete(tempPath);

    return new UploadResultCloud
    {
      PublicId = resultVideo.PublicId,
      Url = resultVideo.Url.ToString()
    };
  }


  public static async Task<UploadResultCloud> UploadSingle(IFormFile file)
  {
    if (file.ContentType.StartsWith("video/"))
      return await UploadVideo(file);
    else if (file.ContentType.StartsWith("image/"))
      return await UploadImage(file);
    else
      throw new Exception("Unsupported file type");
  }

  public static async Task<
    List<UploadResultCloud>
> UploadMultiple(
    List<IFormFile> files
)
  {
    List<UploadResultCloud> results =
        [];

    foreach (IFormFile file in files)
    {
      results.Add(await UploadSingle(file));
    }

    return results;
  }
}