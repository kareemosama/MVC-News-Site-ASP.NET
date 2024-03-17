using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class CustomAuthorizeAttribute : TypeFilterAttribute
{
    public CustomAuthorizeAttribute() : base(typeof(CustomAuthorizeFilter))
    {
    }

    private class CustomAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
               
                context.Result = new RedirectResult("~/Account/Login");
                context.HttpContext.Items["ErrorMessage"] = "Please log in to access this page. ";

            }
            else
            {
               
                context.Result = new RedirectResult("~/Account/AccessDenied");

            }
        }
    }
}
