using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// using MoviesDotnetMvcDemo.Database;
using BookishDotnetMvc.Models;
using BookishDotnetMvc.ViewModels;

namespace BookishDotnetMvc.Controllers
{
    public class BookController : Controller
    {
        // private readonly BooksDbContext _context;

        // public BooksController(BooksDbContext context)
        // {
        //     _context = context;
        // }

        // GET: Movies
        public IActionResult Index()
        {
            // List<BookViewModel> books = [];
            var books = new List<BookViewModel>
            { new BookViewModel { Id=1, Title ="Something1", Author = "sdfsdafsd"}, 
              new BookViewModel  {Id=2, Title ="Something2", Author = "sdfdsfasd"}};
            // Book book1 = new Book {
            //     Id = 1,
            //     Title = "Something",
            //     Author = new Person {Id = 1, Name = "Author 1"},
            // };
            // List<MovieViewModel> movies = (await _context.Movie.Include(movie => movie.Director).ToListAsync()).Select(movie => new MovieViewModel(movie)).ToList();
            return View(new BookViewModel { Id=1, Title ="Something1", Author = "sdfsdafsd"});
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // // POST: Movies/Create
        // [HttpPost]
        // public async Task<IActionResult> Create([Bind("Title,DirectorName,Duration,Genre")] MovieViewModel movieViewModel)
        // {
        //     if (!ModelState.IsValid) {
        //         return View(movieViewModel);
        //     }
        //     Person? director = _context.Person.Where(person => person.Name == movieViewModel.DirectorName).FirstOrDefault();
        //     if (director == null) {
        //         director = new Person() { Name = movieViewModel.DirectorName };
        //         _context.Person.Add(director);
        //     }
        //     _context.Movie.Add(new Movie(movieViewModel, director));
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }
    }
}