using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KandangMobil.Filters
{
    public class AdminAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session.GetInt32("AdminId");

            if (session == null)
            {
                context.Result = new RedirectToActionResult("Index", "AuthAdmin", null);
            }
        }
    }
}
