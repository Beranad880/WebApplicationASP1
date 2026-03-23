using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplicationASP1.Models;
using WebApplicationASP1.Settings;

namespace WebApplicationASP1.Services;

public sealed class CategoryService
{
    private readonly IMongoCollection<Category> _categories;

    public CategoryService(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _categories = database.GetCollection<Category>("categories");
    }

    public async Task<List<Category>> GetAllAsync() =>
        await _categories.Find(_ => true).ToListAsync();
        
    public async Task<Category?> GetByIdAsync(string id) =>
        await _categories.Find(p => p.Id == id).FirstOrDefaultAsync();

    public async Task<Category> CreateAsync(Category category)
    {
        await _categories.InsertOneAsync(category);
        return category;
    }

    public async Task<bool> UpdateAsync(string id, Category updated)
    {
        updated.Id = id;
        var result = await _categories.ReplaceOneAsync(p => p.Id == id, updated);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _categories.DeleteOneAsync(p => p.Id == id);
        return result.DeletedCount > 0;
    }
}
