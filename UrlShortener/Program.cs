using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiDbContext>(o =>
    o.UseSqlServer(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.MapPost("/api/shorten", async (UrlDto url, ApiDbContext dbContext, HttpContext ctx) =>
{
    if (!Uri.TryCreate(url.Url, UriKind.Absolute, out var inputUrl))
    {
        return Results.BadRequest("Invalid url has been provided.");
    }

    var random = new Random();
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    string randomString = new(Enumerable.Repeat(chars, 8)
        .Select(x => x[random.Next(x.Length)]).ToArray());

    var sUrl = new ShortenUrlRequest()
    {
        Url = url.Url,
        ShortUrl = randomString,
    };

    dbContext.Urls.Add(sUrl);
    dbContext.SaveChanges();

    var result = $"{ctx.Request.Scheme}://{ctx.Request.Host}/{sUrl.ShortUrl}";

    return Results.Ok(new UrlShortResponseDto()
    {
        Url = result
    });
});


app.MapFallback(async (ApiDbContext db, HttpContext ctx) =>
{
    var path = ctx.Request.Path.ToUriComponent().Trim('/');
    var urlMatch = await db.Urls.FirstOrDefaultAsync(x =>
        x.ShortUrl.Trim() == path.Trim());

    if (urlMatch == null)
    {
        return Results.BadRequest("Invalid request");
    }

    return Results.Redirect(urlMatch.Url);
});


app.Run();


class ApiDbContext : DbContext
{
    public virtual DbSet<ShortenUrlRequest> Urls { get; set; }

    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {
    }
}