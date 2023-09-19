namespace UrlShortener.Models;

public class ShortenUrlRequest
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string ShortUrl { get; set; } = string.Empty;
}

