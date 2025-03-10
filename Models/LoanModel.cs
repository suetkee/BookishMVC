using BookishDotnetMvc.Models;
using BookishDotnetMvc.ViewModels;

namespace BookishDotnetMvc.Models;

public class Loan {
    public int Id { get; set; }
    public Copy Copy { get; set; }
    public Member Member { get; set; }
    public DateTime CheckoutDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public Loan(Copy copy, Member member) {
        Copy = copy;
        Member = member;
        CheckoutDate = DateTime.Today;
        DueDate = CheckoutDate.AddDays(21);
    }

     public Loan() {}
}