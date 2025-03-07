using BookishDotnetMvc.Models;
using BookishDotnetMvc.ViewModels;

namespace BookishDotnetMvc.Models;

public class Copy {
    public int Id { get; set; }
    public Book Book { get; set; }
    public Member Member { get; set; }
    public DateTime CheckoutDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }


}