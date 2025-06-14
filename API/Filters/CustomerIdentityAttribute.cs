using Microsoft.AspNetCore.Mvc.Filters;

public class CustomerIdentityAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var httpContext = context.HttpContext;
        var services = httpContext.RequestServices;
        var customerIdentity = services.GetService<ICustomerContext>();
        if (customerIdentity == null)
        {
            throw new InvalidOperationException("ICustomerContext service is not registered.");
        }

        if (httpContext.Request.Headers.TryGetValue("x-customer-id", out var headerValue) && Guid.TryParse(headerValue, out var customerId))
        {

            customerIdentity.CustomerId = customerId;
        }
        else
        {
            customerIdentity.CustomerId = Guid.NewGuid();
        }
    }
}
