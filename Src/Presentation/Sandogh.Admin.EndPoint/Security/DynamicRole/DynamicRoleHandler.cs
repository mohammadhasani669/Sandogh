using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Sandogh.Admin.EndPoint.Security.DynamicRole
{
    public class DynamicRoleHandler : AuthorizationHandler<DynamicRoleRequirement>
    {
        private readonly IHttpContextAccessor _contextAccessor;
    

        public DynamicRoleHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
           
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DynamicRoleRequirement requirement)
        {
            var httpContext = _contextAccessor.HttpContext;
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return;

            SplitUserRequestedUrl(httpContext, out var areaAndActionAndControllerName);

          if (httpContext.User.HasClaim(areaAndActionAndControllerName, true.ToString()))
                context.Succeed(requirement);

            return;
        }

        private void SplitUserRequestedUrl(HttpContext httpContext, out string areaAndControllerAndActionName)
        {
           
            var controllerName = httpContext.Request.RouteValues["controller"].ToString() + "Controller";
            var actionName = httpContext.Request.RouteValues["action"].ToString();
            areaAndControllerAndActionName = $"{controllerName}|{actionName}".ToUpper();
        }
    }
}
