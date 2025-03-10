using BookishDotnetMvc.Models;

namespace BookishDotnetMvc.ViewModels;

public class BookViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int PublicationYear { get; set; }
    public string Genre { get; set; }
    public ICollection<Copy>? Copies { get; set; }
    public BookViewModel() {}
    public BookViewModel(Book book) {
        Id = book.Id;
        Title = book.Title;
        Author = book.Author.Name;
        PublicationYear = book.PublicationYear;
        Genre = book.Genre;
        Copies = book.Copies;
    }
}