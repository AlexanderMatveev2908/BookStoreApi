namespace BOOKSTORE_API.ServicesNamespace
.RateLimitNamespace;

public sealed class RateLimitData
{
  public long Now { get; }

  public long WindowMs { get; }

  public string Key { get; }

  public RateLimitData(
      long now,
      long windowMs,
      string key
  )
  {
    Now = now;
    WindowMs = windowMs;
    Key = key;
  }

  public long Expired()
  {
    return Now - WindowMs;
  }
}