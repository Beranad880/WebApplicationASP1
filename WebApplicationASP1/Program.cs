using WebApplicationASP1.Services;
using WebApplicationASP1.Settings;
using WebApplicationASP1.test;
using static WebApplicationASP1.test.IMyTax;

// Load .env file into environment variables
var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
if (File.Exists(envPath))
{
    foreach (var line in File.ReadAllLines(envPath))
    {
        var trimmed = line.Trim();
        if (trimmed.Length == 0 || trimmed.StartsWith('#')) continue;
        var idx = trimmed.IndexOf('=');
        if (idx < 0) continue;
        Environment.SetEnvironmentVariable(trimmed[..idx].Trim(), trimmed[(idx + 1)..].Trim());
    }
}

var builder = WebApplication.CreateBuilder(args);

// Resolve ${VAR} placeholders in configuration using environment variables
foreach (var kvp in builder.Configuration.AsEnumerable())
{
    if (kvp.Value is null) continue;
    var resolved = System.Text.RegularExpressions.Regex.Replace(
        kvp.Value,
        @"\$\{([^}]+)\}",
        m => Environment.GetEnvironmentVariable(m.Groups[1].Value) ?? m.Value);
    if (resolved != kvp.Value)
        builder.Configuration[kvp.Key] = resolved;
}


// Bind MongoDB settings from appsettings.json
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<TaxSettings>(
    builder.Configuration.GetSection("TaxSettings"));
// Register ProductService as a singleton — MongoClient is thread-safe
// and meant to be reused for the lifetime of the application
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<IMyTax, MyTaxImpl>();
builder.Services.AddControllers();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.MapGet("/", (HttpContext ctx) =>
{
    ctx.Response.Redirect("/Products");
    return Task.CompletedTask;
});
app.Run();
