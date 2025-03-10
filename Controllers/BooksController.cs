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
            List<BookViewModel> books = (await _context.Books.Include(book => book.Author).Include(book => book.Copies).ToListAsync()).Select(book => new BookViewModel(book, book.Copies.Count)).ToList();
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
                foreach (var state in ModelState) {
                    if (state.Value.Errors.Count > 0) {
                        foreach (var error in state.Value.Errors) {
                            Console.WriteLine($"Field: {state.Key}, Error: {error.ErrorMessage}");          
                        }
                    }
                    
                }
                return View(bookViewModel);
            }
            Author? author = _context.Author.Where(person => person.Name == bookViewModel.Author).FirstOrDefault();
            if (author == null) {
                author = new Author() { Name = bookViewModel.Author };
                _context.Author.Add(author);
            }
            var book = new Book(bookViewModel, author);
            _context.Books.Add(book);
            for (int i = 0; i < bookViewModel.Copies; i++ ) {
                _context.Copy.Add(new Copy() {Book = book});
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,PublicationYear,Genre, Copies")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            // if(!ModelState.IsValid) {
            //     foreach (var state in ModelState) {
            //         if (state.Value.Errors.Count > 0) {
            //             foreach (var error in state.Value.Errors) {
            //                 Console.WriteLine($"Field: {state.Key}, Error: {error.ErrorMessage}");          
            //             }
            //         }
                    
            //     }
            // }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

    }
}