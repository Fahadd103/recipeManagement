import { useState, useEffect } from 'react';
import SearchBar from '../components/SearchBar';
import CategoryFilter from '../components/CategoryFilter';
import RecipeCard from '../components/RecipeCard';
import { getRecipes, searchRecipes } from '../utils/api';

function Home() {
  const [recipes, setRecipes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchRecipes = async () => {
        try {
          setLoading(true);
          const data = await getRecipes();
          setRecipes(data);
        } catch (err) {
          setError('Failed to fetch recipes');
        } finally {
          setLoading(false);
        }
      };
    fetchRecipes();
  }, []);

  

  const handleSearch = async (query, category) => {
    try {
      setLoading(true);
      const data = await searchRecipes(query, category);
      setRecipes(data);
    } catch (err) {
      setError('Failed to search recipes');
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>{error}</div>;

  return (
    <div>
      <h1 className="text-3xl font-bold mb-8">Discover Recipes</h1>
      <div className="mb-8">
        <SearchBar onSearch={handleSearch} />
        <CategoryFilter onFilter={(category) => handleSearch('', category)} />
      </div>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
        {recipes.map(recipe => (
          <RecipeCard key={recipe.id} recipe={recipe} />
        ))}
      </div>
    </div>
  );
}

export default Home;