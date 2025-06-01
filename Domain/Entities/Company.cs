namespace Domain.Entities;

public class Company
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public virtual ApplicationConfiguration? ApplicationConfiguration { get; set; }
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
