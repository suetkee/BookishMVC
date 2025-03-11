using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookish;
using BookishDotnetMvc.Models;
using BookishDotnetMvc.ViewModels;

namespace MVC.Controllers
{
    public class MembersController : Controller
    {
        private readonly BookishContext _context;

        public MembersController(BookishContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            return View(await _context.Member.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member.Include(member => member.Loans!).ThenInclude(loan => loan.Copy).ThenInclude(copy => copy.Book).FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,dateOfBirth,Email")] MemberViewModel member)
        {
            if (!ModelState.IsValid) {
                return View(member);
            }

            // _context.Add(member);
            _context.Add(new Member(member, []));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            // return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,dateOfBirth,Email")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if(!ModelState.IsValid) {
                foreach (var state in ModelState) {
                    if (state.Value.Errors.Count > 0) {
                        foreach (var error in state.Value.Errors) {
                            Console.WriteLine($"Field: {state.Key}, Error: {error.ErrorMessage}");          
                        }
                    }
                    
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Member.FindAsync(id);
            if (member != null)
            {
                _context.Member.Remove(member);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.Id == id);
        }

        // GET: Members/AddLoan
        public IActionResult AddLoan(int? id)
        {
            return View();
        }

        // POST: Members/AddLoan
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLoan(int id, [Bind("Barcode")] LoanViewModel loanViewModelModel)
        {
            if (!ModelState.IsValid) {
                return View(loanViewModelModel);
            }
            Member? member = _context.Member.Where(member => member.Id == id).FirstOrDefault();
            Copy? copy = _context.Copy.Where(copy => copy.Barcode == loanViewModelModel.Barcode).FirstOrDefault();
            if (copy == null || member == null) {
               return NotFound();
            } 

            var loan = new Loan(copy, member);
            _context.Add(loan);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}
