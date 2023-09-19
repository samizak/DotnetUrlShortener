using Microsoft.EntityFrameworkCore;

namespace UrlShortener;

public class UrlShorteningService
{
    //public const int NumberOfCharactersInShortLink = 7;
    //private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    //private readonly Random _random = new();
    //private readonly ApplicationDbContext _dbContext;

    //public UrlShorteningService(ApplicationDbContext dbContext)
    //{
    //    _dbContext = dbContext;
    //}

    //public async Task<string> GenerateUniqueCode()
    //{
    //    var codeChars = new char[NumberOfCharactersInShortLink];

    //    while(true)
    //    {
        
    //        for(int i = 0; i< NumberOfCharactersInShortLink; i++)
    //        {
    //            var randomIndex = _random.Next(Alphabet.Length - 1);

    //            codeChars[i] = Alphabet[randomIndex];
    //        }

    //        var code = new string(codeChars);

    //        if (!await _dbContext.ShortenedUrls.AnyAsync(s => s.Code == code))
    //        {
    //            return code;
    //        }
    //    }

    //}

}

