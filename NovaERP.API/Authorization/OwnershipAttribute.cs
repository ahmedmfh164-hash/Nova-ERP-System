using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using ERP.Core.Enums;
public class OwnershipAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.FindFirstValue(ClaimTypes.Role)==Roles.Admin.ToString())
            return;

        var currentUserId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var routeId = context.RouteData.Values["UserId"]?.ToString();

        if (currentUserId == null || routeId == null ||currentUserId != routeId)
        {
            context.Result = new ForbidResult();
        }
    }
}