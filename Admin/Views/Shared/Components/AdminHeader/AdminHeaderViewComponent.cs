using KandangMobil.Filters;
using KandangMobil.Helpers;
using KandangMobil.Interfaces;
using KandangMobil.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.Master;

namespace KandangMobil.ViewComponents
{
    public class AdminHeaderViewComponent : ViewComponent
    {
        private readonly IMasterAdmin _IMasterAdmin;

        public AdminHeaderViewComponent(IMasterAdmin iMasterAdmin)
        {
            _IMasterAdmin = iMasterAdmin;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int? adminId = HttpContext.Session.GetInt32("AdminId");
            if (adminId == null)
                return View(null);

            var admin = await _IMasterAdmin.Find(adminId.Value);
            return View(admin);
        }
    }
}
