using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookishDotnetMvc.Models;
using BookishDotnetMvc.ViewModels;
using Bookish;

namespace BookishDotnetMvc.Controllers
{
    public class BooksController : Controller
    {

        private readonly BookishContext _context;

        public BooksController(BookishContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            List<BookViewModel> books = (await _context.Books.Include(book => book.Author).ToListAsync()).Select(movie => new BookViewModel(movie, [])).ToList();
            return View(books);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Author,PublicationYear,Genre")] BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid) {
                return View(bookViewModel);
            }
            Author? author = _context.Author.Where(person => person.Name == bookViewModel.Author).FirstOrDefault();
            if (author == null) {
                author = new Author() { Name = bookViewModel.Author };
                _context.Author.Add(author);
            }
            _context.Books.Add(new Book(bookViewModel, author));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}