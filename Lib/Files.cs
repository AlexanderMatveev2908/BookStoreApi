namespace BOOKSTORE_API.FilesLibNamespace;

public static class FilesLib
{
  public static string MakeNewFilename(IFormFile file)
  {
    string ext =
    Path.GetExtension(
        file.FileName
    );
    string fileName =
        $"{Guid.NewGuid():N}{ext}";

    return fileName;
  }
}