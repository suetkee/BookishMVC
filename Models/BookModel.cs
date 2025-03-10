using BookishDotnetMvc.ViewModels;

namespace BookishDotnetMvc.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Author Author { get; set; }
    public int PublicationYear { get; set; }
    public string Genre { get; set; }
    public ICollection<Copy> Copies { get; set; }

    public Book(BookViewModel bookViewModel, Author author) {
        Id = bookViewModel.Id;
        Title = bookViewModel.Title;
        Author = author;
        PublicationYear = bookViewModel.PublicationYear;
        Genre = bookViewModel.Genre;
    }
     public Book() {}
}