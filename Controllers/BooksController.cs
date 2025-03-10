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
            List<BookViewModel> books = (await _context.Books.Include(book => book.Author).Include(book => book.Copies).ToListAsync()).Select(book => new BookViewModel(book)).ToList();
            return View(books);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Author,PublicationYear,Genre, Copies")] BookViewModel bookViewModel)
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


        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.Include(book => book.Author).FirstOrDefaultAsync(book => book.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(new BookViewModel(book));
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,PublicationYear,Genre")] BookViewModel bookViewModel)
        {
            if (id != bookViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Author? author = _context.Author.Where(person => person.Name == bookViewModel.Author).FirstOrDefault();
                    if (author == null)
                    {
                        author = new Author() { Name = bookViewModel.Author };
                        _context.Author.Add(author);
                    }
                    var book = new Book(bookViewModel, author);
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(bookViewModel.Id))
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
            return View(bookViewModel);
        }

        // GET: Books/AddCopy/5
        public async Task<IActionResult> AddCopy(int? id)
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

            return View();
        }

        // POST: Books/AddCopy/5
        [HttpPost]
        public async Task<IActionResult> AddCopy(int id, [Bind("Barcode")] CopyViewModel copyViewModel)
        {
            var book = await _context.Books.FindAsync(id);
            
            if (book == null || id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var copy = new Copy() {Barcode = copyViewModel.Barcode, Book = book};
                _context.Copy.Add(copy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(copyViewModel);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.Include(book => book.Author).FirstOrDefaultAsync(book => book.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

    }
}