using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipeManagementSystem.Models
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public required string Title { get; set; }  // Added 'required' modifier
        public List<string> Ingredients { get; set; } = new List<string>();
        public List<string> Steps { get; set; } = new List<string>();
        public int PreparationTime { get; set; }
        public required string Category { get; set; }  // Added 'required' modifier
        public string UserId { get; set; } = string.Empty; // This will be set during recipe creation
        public required string ImageUrl { get; set; }  // Added 'required' modifier
    }
}