import React from 'react';
import { Link } from 'react-router-dom';

function RecipeCard({ recipe }) {
  return (
    <div className="bg-white shadow-md rounded-lg overflow-hidden">
      <img src={recipe.imageUrl} alt={recipe.title} className="w-full h-48 object-cover" />
      <div className="p-4">
        <h3 className="text-xl font-semibold mb-2">{recipe.title}</h3>
        <p className="text-gray-600 mb-2">Prep time: {recipe.preparationTime} mins</p>
        <p className="text-gray-600 mb-4">Category: {recipe.category}</p>
        <Link to={`/recipe/${recipe.id}`} className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600">
          View Recipe
        </Link>
      </div>
    </div>
  );
}

export default RecipeCard;