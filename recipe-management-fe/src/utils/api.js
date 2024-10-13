const API_URL = 'http://localhost:5294/api';

async function fetchWithAuth(url, options = {}) {
  const token = localStorage.getItem('token');
  const headers = {
    'Content-Type': 'application/json',
    ...options.headers,
  };

  if (token) {
    headers['Authorization'] = `Bearer ${token}`;
  }

  const response = await fetch(`${API_URL}${url}`, {
    ...options,
    headers,
  });

  if (!response.ok) {
    throw new Error('API request failed');
  }
  
  if(response.status === 204){
    return null;
  }

  return response.json();
}

export const login = (credentials) => 
  fetchWithAuth('/auth/login', { method: 'POST', body: JSON.stringify(credentials) });

export const register = (userData) => 
  fetchWithAuth('/auth/register', { method: 'POST', body: JSON.stringify(userData) });

export const getRecipes = () => 
  fetchWithAuth('/recipe');

export const getRecipe = (id) => 
  fetchWithAuth(`/recipe/${id}`);

export const createRecipe = (recipeData) => 
  fetchWithAuth('/recipe', { method: 'POST', body: JSON.stringify(recipeData) });

export const updateRecipe = (id, recipeData) => 
  fetchWithAuth(`/recipe/${id}`, { method: 'PUT', body: JSON.stringify(recipeData) });

export const deleteRecipe = (id) => 
  fetchWithAuth(`/recipe/${id}`, { method: 'DELETE' });

export const searchRecipes = (query) => 
  fetchWithAuth(`/recipe/search?query=${query}`);