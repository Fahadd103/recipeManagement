using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipeManagementSystem.Models
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public required string Title { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();
        public List<string> Steps { get; set; } = new List<string>();
        public int PreparationTime { get; set; }
        public required string Category { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty; 
        public required string ImageUrl { get; set; } = string.Empty;
    }
}