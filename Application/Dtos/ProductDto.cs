public class ProductDto
{
    public required string Name { get; set; }
    public int Point { get; set; }
    public Guid Id { get; private set; }

    public static ProductDto FromEntity(Domain.Entities.Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Point = product.Point
        };
    }

    public Domain.Entities.Product ToEntity()
    {
        return Domain.Entities.Product.Create(Name, Point);
    }
}