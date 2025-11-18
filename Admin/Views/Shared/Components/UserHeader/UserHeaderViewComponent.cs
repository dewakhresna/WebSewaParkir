using KandangMobil.Filters;
using KandangMobil.Helpers;
using KandangMobil.Interfaces;
using KandangMobil.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.Master;

namespace KandangMobil.ViewComponents
{
    public class UserHeaderViewComponent : ViewComponent
    {
        private readonly IMasterUser _IMasterUser;

        public UserHeaderViewComponent(IMasterUser iMasterUser)
        {
            _IMasterUser = iMasterUser;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return View(null);

            var user = await _IMasterUser.Find(userId.Value);
            return View(user);
        }
    }
}
