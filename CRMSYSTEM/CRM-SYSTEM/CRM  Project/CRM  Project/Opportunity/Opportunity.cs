public enum Stage { New, Qualified, Won, Lost }

public class Opportunity
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public Stage Stage { get; set; }

    public Opportunity(int id, int customerId, string title, decimal amount, Stage stage)
    {
        Id = id;
        CustomerId = customerId;
        Title = title;
        Amount = amount;
        Stage = stage;
    }
   
}
