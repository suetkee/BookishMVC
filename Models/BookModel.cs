using BookishDotnetMvc.ViewModels;

namespace BookishDotnetMvc.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Person Author { get; set; }
    public int PublicationYear { get; set; }
    public string Genre { get; set; }

    public Book(BookViewModel bookViewModel, Person person) {
        Id = bookViewModel.Id;
        Title = bookViewModel.Title;
        Author = person;
        PublicationYear = bookViewModel.PublicationYear;
        Genre = bookViewModel.Genre;
    }
     public Book() {}
}