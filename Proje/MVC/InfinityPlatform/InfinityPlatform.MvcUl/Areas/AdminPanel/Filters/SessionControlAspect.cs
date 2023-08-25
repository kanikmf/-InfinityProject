using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Filters
{
    public class SessionControlAspect : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionData = context.HttpContext.Session.GetString("ActiveAdminPanelUser");

            if (string.IsNullOrEmpty(sessionData))
                context.Result = new RedirectToActionResult("LogIn", "Authentication", null);

        }
    }
}
