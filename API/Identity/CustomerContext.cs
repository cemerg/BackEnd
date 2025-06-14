public class CustomerContext : ICustomerContext
{
    public Guid CustomerId { get; set; }
}

public class AdminContext : IAdminContext
{
    public Guid AdminUserId { get; set; }
}