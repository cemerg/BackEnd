using Microsoft.AspNetCore.Mvc.Filters;

public class AdminIdentityAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var httpContext = context.HttpContext;
        var services = httpContext.RequestServices;
        var adminContext = services.GetService<IAdminContext>();
        if (adminContext == null)
        {
            throw new InvalidOperationException("IAdminContext service is not registered.");
        }
        if (httpContext.Request.Headers.TryGetValue("x-admin-user-id", out var headerValue) && Guid.TryParse(headerValue, out var adminUserId))
        {
            adminContext.AdminUserId = adminUserId;
        }
    }
}
