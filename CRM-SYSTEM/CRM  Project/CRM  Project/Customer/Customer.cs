public record Email(string Address);
public record Phone(string Number);
public record Address(string Street, string City, string Country);

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Email Email { get; set; }
    public Phone Phone { get; set; }
    public Address Address { get; set; }
    public string Industry { get; set; }
    public int NumberOfEmployees { get; set; }

   
    public Customer(int id, string name, Email email, Phone phone, Address address, string industry, int numberOfEmployees)
    {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
        Industry = industry;
        NumberOfEmployees = numberOfEmployees;
    }
}