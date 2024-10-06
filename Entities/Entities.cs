// RecipeManagementSystem.Core/Entities/User.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipeManagementSystem.Core.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}

// RecipeManagementSystem.Core/Entities/Recipe.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace RecipeManagementSystem.Core.Entities
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        public int PreparationTime { get; set; }
        public string Category { get; set; }
        public string UserId { get; set; }
        public string ImageUrl { get; set; }
    }
}

// RecipeManagementSystem.Core/Entities/Category.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipeManagementSystem.Core.Entities
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}