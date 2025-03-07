using BookishDotnetMvc.Models;

namespace BookishDotnetMvc.ViewModels;

public class MemberViewModel
{

    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly dateOfBirth { get; set; }
    public string Email { get; set; }
    public ICollection<Copy> Loans { get; set; }

    public MemberViewModel() {}
    public MemberViewModel(Member member, ICollection<Copy> loans) {
        Id = member.Id;
        Name = member.Name;
        dateOfBirth = member.dateOfBirth;
        Email = member.Email;
        Loans=loans;
    }
}