namespace BookishDotnetMvc.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly dateOfBirth { get; set; }
    public ICollection<Book> Books { get; set; }
}