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