import "./App.css";
import React, { useState, useEffect } from "react";
import { getBooks, createBook, updateBook, deleteBook } from "./BookService";

function App() {
  const [books, setBooks] = useState([]);
  const [formData, setFormData] = useState({
    title: "",
    author: "",
    price: "",
  });
  const [editingId, setEditingId] = useState(null);

  useEffect(() => {
    fetchBooks();
  }, []);

  const fetchBooks = async () => {
    try {
      const response = await getBooks();
      setBooks(response.data);
    } catch (error) {
      console.error("Error fetching books:", error);
    }
  };

  const handleChange = (e) =>
    setFormData({ ...formData, [e.target.name]: e.target.value });

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (editingId) {
        await updateBook(editingId, formData);
      } else {
        await createBook(formData);
      }
      setFormData({ title: "", author: "", price: "" });
      setEditingId(null);
      fetchBooks();
    } catch (error) {
      console.error("Error saving book:", error);
    }
  };

  const handleEdit = (book) => {
    setFormData({ title: book.title, author: book.author, price: book.price });
    setEditingId(book.id);
  };

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this book?")) {
      await deleteBook(id);
      fetchBooks();
    }
  };

  return (
    <div className="min-h-screen bg-gray-100 p-6">
      <div className="max-w-5xl mx-auto">
        <h1 className="text-5xl font-extrabold text-center text-blue-600 mb-10">
          📚 Modern Book Manager
        </h1>

        {/* Form Card */}
        <div className="bg-white p-8 rounded-xl shadow-lg mb-10">
          <h2 className="text-2xl font-semibold mb-6">
            {editingId ? "Edit Book" : "Add New Book"}
          </h2>
          <form
            className="grid grid-cols-1 md:grid-cols-3 gap-4"
            onSubmit={handleSubmit}
          >
            <input
              name="title"
              placeholder="Title"
              value={formData.title}
              onChange={handleChange}
              required
              className="p-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
            <input
              name="author"
              placeholder="Author"
              value={formData.author}
              onChange={handleChange}
              required
              className="p-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
            <div className="flex gap-4">
              <input
                name="price"
                type="number"
                step="0.01"
                placeholder="Price"
                value={formData.price}
                onChange={handleChange}
                required
                className="flex-1 p-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
              <button
                type="submit"
                className={`px-6 py-3 rounded-lg text-white font-semibold transition ${
                  editingId
                    ? "bg-yellow-500 hover:bg-yellow-600"
                    : "bg-blue-600 hover:bg-blue-700"
                }`}
              >
                {editingId ? "Update" : "Add"}
              </button>
            </div>
          </form>
        </div>

        {/* Books Table */}
        <div className="overflow-x-auto">
          <table className="min-w-full bg-white rounded-xl shadow-lg overflow-hidden">
            <thead className="bg-blue-600 text-white text-left">
              <tr>
                <th className="py-3 px-6">Title</th>
                <th className="py-3 px-6">Author</th>
                <th className="py-3 px-6">Price</th>
                <th className="py-3 px-6">Actions</th>
              </tr>
            </thead>
            <tbody>
              {books.length === 0 ? (
                <tr>
                  <td colSpan="4" className="text-center py-6 text-gray-500">
                    No books available
                  </td>
                </tr>
              ) : (
                books.map((book, index) => (
                  <tr
                    key={book.id}
                    className={`border-b ${
                      index % 2 === 0 ? "bg-gray-50" : "bg-gray-100"
                    } hover:bg-gray-200 transition`}
                  >
                    <td className="py-3 px-6">{book.title}</td>
                    <td className="py-3 px-6">{book.author}</td>
                    <td className="py-3 px-6">${book.price}</td>
                    <td className="py-3 px-6 flex gap-2">
                      <button
                        onClick={() => handleEdit(book)}
                        className="px-3 py-1 bg-yellow-400 rounded-lg hover:bg-yellow-500 text-white font-semibold transition"
                      >
                        Edit
                      </button>
                      <button
                        onClick={() => handleDelete(book.id)}
                        className="px-3 py-1 bg-red-500 rounded-lg hover:bg-red-600 text-white font-semibold transition"
                      >
                        Delete
                      </button>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default App;
