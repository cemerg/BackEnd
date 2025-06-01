namespace Domain.Entities;

public class ApplicationConfiguration
{
    public Guid Id { get; set; }
    public required string ApplicationTitle { get; set; }
    public required string HomePageImageUrl { get; set; }
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; }
}
