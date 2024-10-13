using RecipeManagementSystem.Models;
using MongoDB.Driver;

namespace RecipeManagementSystem.Repositories
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<Recipe> GetRecipeByIdAsync(string id);
        Task<Recipe> CreateRecipeAsync(Recipe recipe);
        Task UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(string id);
        Task<IEnumerable<Recipe>> SearchRecipesAsync(string query);
    }
}



namespace RecipeManagementSystem.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly IMongoCollection<Recipe> _recipes;

        public RecipeRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoDbSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("MongoDbSettings:DatabaseName"));
            _recipes = database.GetCollection<Recipe>("Recipes");
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _recipes.Find(_ => true).ToListAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(string id)
        {
            return await _recipes.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            await _recipes.InsertOneAsync(recipe);
            return recipe;
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            await _recipes.ReplaceOneAsync(r => r.Id == recipe.Id, recipe);
        }

        public async Task DeleteRecipeAsync(string id)
        {
            await _recipes.DeleteOneAsync(r => r.Id == id);
        }


        public async Task<IEnumerable<Recipe>> SearchRecipesAsync(string query)
        {
            var filter = Builders<Recipe>.Filter.Empty;

            if (!string.IsNullOrEmpty(query))
            {
                filter = filter & (Builders<Recipe>.Filter.Regex(r => r.Title, new MongoDB.Bson.BsonRegularExpression(query, "i")) |
                                   Builders<Recipe>.Filter.AnyEq(r => r.Ingredients, query));
            }

            return await _recipes.Find(filter).ToListAsync();
        }
    }
}