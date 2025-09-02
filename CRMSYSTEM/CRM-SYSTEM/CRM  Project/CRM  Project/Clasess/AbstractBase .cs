namespace CRM__Project.Clasess
{
    public abstract class ActivityBase
    {
public int Id { get; set; }
public DateTime Date { get; set; }
protected abstract void Validate();
protected ActivityBase(int id, string description, DateTime date)
    {
      Id = id;

        Date = date;
        }
    }
}