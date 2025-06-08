namespace Domain.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public int Point { get; set; }

    public static Product Create(string name, int point)
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Point = point
        };
    }
    
}
