using WebApplicationASP1.Services;
using WebApplicationASP1.Settings;
using WebApplicationASP1.test;
using static WebApplicationASP1.test.IMyTax;

var builder = WebApplication.CreateBuilder(args);


// Bind MongoDB settings from appsettings.json
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<TaxSettings>(
    builder.Configuration.GetSection("TaxSettings"));
// Register ProductService as a singleton — MongoClient is thread-safe
// and meant to be reused for the lifetime of the application
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IMyTax, MyTaxImpl>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", (HttpContext ctx) =>
{
    ctx.Response.Redirect("/api/products");
    return Task.CompletedTask;
});
app.Run();
