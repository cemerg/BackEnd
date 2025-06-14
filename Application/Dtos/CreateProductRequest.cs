public class CreateProductRequest
{
    public required string Name { get; set; }
    public int Point { get; set; }

    public CreateProductRequest(string name, int point)
    {
        Name = name;
        Point = point;
    }
}

public class UpdateProductRequest
{
    public required string Name { get; set; }
    public int Point { get; set; }

    public UpdateProductRequest(string name, int point)
    {
        Name = name;
        Point = point;
    }
}