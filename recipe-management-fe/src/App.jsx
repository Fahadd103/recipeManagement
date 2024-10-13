import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import AuthProvider from './context/AuthContext';
import Home from './pages/Home';
import Login from './pages/Login';
import Header from './components/Header';
import Footer from './components/Footer';
import Register from './pages/Register';
import RecipeDetail from './pages/RecipeDetail';
import PrivateRoute from './utils/PrivateRoute';
import CreateEditRecipe from './pages/CreateEditRecipe';




function App() {
  return (
    <AuthProvider>
      <Router>
        <div className="flex flex-col min-h-screen">
          <Header />
          <main className="flex-grow container mx-auto px-4 py-8">
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/login" element={<Login />} />
              <Route path="/register" element={<Register />} />
              <Route path="/recipe/:id" element={<RecipeDetail />} />
              <Route path="/create-recipe" element={<PrivateRoute><CreateEditRecipe /></PrivateRoute>} />
              <Route path="/edit-recipe/:id" element={<PrivateRoute><CreateEditRecipe /></PrivateRoute>} />
            </Routes>
          </main>
          <Footer />
        </div>
      </Router>
    </AuthProvider>
  );
}

export default App;