using CRM__Project.Clasess;
using System.Numerics;
namespace CRM.Values
{
    public record Email(string Address);
    public record Phone(string Number);
    public record Address(string Street, string City, string Country);
}
public enum LeadStatus { New, Contacted, Qualified, Lost }
public class Lead : ActivityBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Email Email { get; set; }
    public Phone Phone { get; set; }
    public string Source { get; set; }
    public LeadStatus LeadStatus { get; set; }



    public Lead(int id, string name, Email email, Phone phone, string source, LeadStatus status) : base(id, name, DateTime.Now)
    {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
        Source = source;
        LeadStatus = status;

    }
   
    protected override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new ArgumentException("Name is required");

        if (Email == null)
            throw new ArgumentException("Email is required");

        if (Phone == null)
            throw new ArgumentException("Phone is required");

        if (string.IsNullOrWhiteSpace(Source))
            throw new ArgumentException("Source is required");
    }


}