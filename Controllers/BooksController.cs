using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookishDotnetMvc.Models;
using BookishDotnetMvc.ViewModels;

namespace BookishDotnetMvc.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public IActionResult Index()
        {
            var books = new List<BookViewModel>
            { new BookViewModel { Id=1, Title ="The Hitchhiker's Guide to the Galaxy", Author = "Douglas Adams", PublicationYear = 1979, Genre = "Science Fiction"}, 
              new BookViewModel  {Id=2, Title ="The Lord of the Rings", Author = "J.R.R. Tolkien", PublicationYear = 1954, Genre = "Fantasy"},
              new BookViewModel  {Id=3, Title ="Crime and Punishments", Author = "Fyodor Dostoevsky", PublicationYear = 1866, Genre = "psychological drama"},
              new BookViewModel  {Id=4, Title ="Matilda", Author = "Roald Dahl", PublicationYear = 1988, Genre = "Children's Fiction"},};

            return View(books);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }
    }
}