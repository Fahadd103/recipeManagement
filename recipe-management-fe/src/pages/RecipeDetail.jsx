import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getRecipe, deleteRecipe } from '../utils/api';
import { useAuth } from '../context/AuthContext';

function RecipeDetail() {
  const { id } = useParams();
  const navigate = useNavigate();
  const { user } = useAuth();
  const [recipe, setRecipe] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    fetchRecipe();
  }, [id]);

  console.log(user);

  const fetchRecipe = async () => {
    try {
      setLoading(true);
      const data = await getRecipe(id);
      setRecipe(data);
    } catch (err) {
      setError('Failed to fetch recipe details');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async () => {
    if (window.confirm('Are you sure you want to delete this recipe?')) {
      try {
        await deleteRecipe(id);
        navigate('/');
      } catch (err) {
        setError('Failed to delete recipe');
      }
    }
  };

  const handleEdit = () => {
    navigate(`/edit-recipe/${id}`);
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>{error}</div>;
  if (!recipe) return <div>Recipe not found</div>;

  // Assuming the JWT contains a 'nameid' claim for the user ID
  const isOwner = user && user['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
  console.log(isOwner);
  return (
    <div className="max-w-2xl mx-auto">
      <h1 className="text-3xl font-bold mb-4">{recipe.title}</h1>
      <img src={recipe.imageUrl} alt={recipe.title} className="w-full h-64 object-cover mb-4 rounded" />
      <p className="mb-2"><strong>Preparation Time:</strong> {recipe.preparationTime} minutes</p>
      <p className="mb-2"><strong>Category:</strong> {recipe.category}</p>
      <h2 className="text-2xl font-semibold mt-4 mb-2">Ingredients</h2>
      <ul className="list-disc pl-5 mb-4">
        {recipe.ingredients.map((ingredient, index) => (
          <li key={index}>{ingredient}</li>
        ))}
      </ul>
      <h2 className="text-2xl font-semibold mt-4 mb-2">Steps</h2>
      <ol className="list-decimal pl-5 mb-4">
        {recipe.steps.map((step, index) => (
          <li key={index}>{step}</li>
        ))}
      </ol>
      {isOwner && (
        <div className="mt-4 space-x-4">
          <button
            onClick={handleEdit}
            className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
          >
            Edit Recipe
          </button>
          <button
            onClick={handleDelete}
            className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600"
          >
            Delete Recipe
          </button>
        </div>
      )}
    </div>
  );
}

export default RecipeDetail;