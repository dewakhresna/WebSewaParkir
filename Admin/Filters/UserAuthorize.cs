using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KandangMobil.Filters
{
    public class UserAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session.GetInt32("UserId");

            if (session == null)
            {
                context.Result = new RedirectToActionResult("Index", "AuthUser", null);
            }
        }
    }
}
