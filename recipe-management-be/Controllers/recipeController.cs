using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeManagementSystem.Models;
using RecipeManagementSystem.Repositories;
using System.Security.Claims;

namespace RecipeManagementSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _recipeRepository.GetAllRecipesAsync();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(string id)
        {
            var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] Recipe recipe)
        {
            recipe.UserId = GetCurrentUserId(); 
            await _recipeRepository.CreateRecipeAsync(recipe);
            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
        }

        private string GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(string id, Recipe recipe)
        {
            var existingRecipe = await _recipeRepository.GetRecipeByIdAsync(id);
            Console.WriteLine(id, existingRecipe.UserId);
            if (existingRecipe == null)
            {
                return NotFound();
            }

            if (existingRecipe.UserId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            {
                return Forbid();
            }

            recipe.Id = id;
            recipe.UserId = existingRecipe.UserId;
            await _recipeRepository.UpdateRecipeAsync(recipe);
            return Ok(recipe);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(string id)
        {
            var existingRecipe = await _recipeRepository.GetRecipeByIdAsync(id);
            if (existingRecipe == null)
            {
                return NotFound();
            }

            if (existingRecipe.UserId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            {
                return Forbid();
            }

            await _recipeRepository.DeleteRecipeAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRecipes([FromQuery] string query)
        {
            var recipes = await _recipeRepository.SearchRecipesAsync(query);
            return Ok(recipes);
        }
    }
}