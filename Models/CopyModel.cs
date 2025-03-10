using BookishDotnetMvc.Models;
using BookishDotnetMvc.ViewModels;

namespace BookishDotnetMvc.Models;

public class Copy {
    public int Id { get; set; }
    public Book Book { get; set; }
}