namespace Domain.Entities;

public class Customer
{
    public Guid Id { get; set; }
    public string? ExternalId { get; set; }
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; }
}
