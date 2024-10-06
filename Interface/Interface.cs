// RecipeManagementSystem.Core/Interfaces/IUserRepository.cs
using RecipeManagementSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagementSystem.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
    }
}

// RecipeManagementSystem.Core/Interfaces/IRecipeRepository.cs
using RecipeManagementSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagementSystem.Core.Interfaces
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetByIdAsync(string id);
        Task<IEnumerable<Recipe>> GetAllAsync();
        Task<IEnumerable<Recipe>> GetByUserIdAsync(string userId);
        Task<IEnumerable<Recipe>> SearchAsync(string searchTerm);
        Task AddAsync(Recipe recipe);
        Task UpdateAsync(Recipe recipe);
        Task DeleteAsync(string id);
    }
}

// RecipeManagementSystem.Core/Interfaces/ICategoryRepository.cs
using RecipeManagementSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagementSystem.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(string id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(string id);
    }
}