using BookishDotnetMvc.Models;
using BookishDotnetMvc.ViewModels;

namespace BookishDotnetMvc.Models;

public class CopyViewModel {
    public int Id { get; set; }
    public int BookId { get; set; }
    public string Barcode { get; set; }
}