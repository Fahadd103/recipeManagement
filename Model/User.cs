using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipeManagementSystem.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Username { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
    }
}