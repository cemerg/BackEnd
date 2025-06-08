namespace Domain.Entities;

public class Configuration : BaseEntity
{
    public required string ApplicationTitle { get; set; }
    public required string HomePageImageUrl { get; set; }
}
