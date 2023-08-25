using Microsoft.AspNetCore.Mvc.Filters;

namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Filters
{
    public class LogAspect : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
        }
    }
}