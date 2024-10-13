
function CategoryFilter({ onFilter }) {
  const categories = ['Appetizer', 'Main Course', 'Dessert', 'Beverage']; // Add more categories as needed

  return (
    <div className="mb-4">
      <h3 className="text-lg font-semibold mb-2">Filter by Category</h3>
      <div className="flex flex-wrap gap-2">
        {categories.map(category => (
          <button
            key={category}
            onClick={() => onFilter(category)}
            className="bg-gray-200 px-3 py-1 rounded hover:bg-gray-300"
          >
            {category}
          </button>
        ))}
      </div>
    </div>
  );
}

export default CategoryFilter;