
public interface ICustomerContext
{
    Guid CustomerId { get; set; }
}

public interface IAdminContext
{
    Guid AdminUserId { get; set; }
}