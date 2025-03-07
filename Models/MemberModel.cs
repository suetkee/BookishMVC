using BookishDotnetMvc.ViewModels;

namespace BookishDotnetMvc.Models;

public class Member
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly dateOfBirth { get; set; }
    public string Email { get; set; }
    public ICollection<Copy> Loans { get; set; }

    public Member(MemberViewModel memberViewModel, ICollection<Copy> loans) {
    Id = memberViewModel.Id;
    Name= memberViewModel.Name;
    dateOfBirth= memberViewModel.dateOfBirth;
    Email= memberViewModel.Email;
    Loans=loans; 
    }
     public Member() {}
}