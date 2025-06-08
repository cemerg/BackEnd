namespace Domain.Entities;

public class TransactionProduct : BaseEntity
{
    public Guid TransactionId { get; set; }
    public Transaction? Transaction { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public static TransactionProduct Create(Guid transactionId, Product product, int quantity)
    {
        return new TransactionProduct
        {
            Id = Guid.NewGuid(),
            TransactionId = transactionId,
            ProductId = product.Id,
            Product = product,
            Quantity = quantity
        };
    }
}