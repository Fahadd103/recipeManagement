using RecipeManagementSystem.Models;
using MongoDB.Driver;

namespace RecipeManagementSystem.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string id);
    }
}



namespace RecipeManagementSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoDbSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("MongoDbSettings:DatabaseName"));
            _users = database.GetCollection<User>("Users");
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }

        public async Task DeleteUserAsync(string id)
        {
            await _users.DeleteOneAsync(u => u.Id == id);
        }
    }
}