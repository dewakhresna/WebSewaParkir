using KandangMobil.Filters;
using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Master;

namespace KandangMobil.Controllers.Master
{
    [AdminAuthorize]
    public class MasterAdminController : Controller
    {
        private readonly IMasterAdmin _IMasterAdmin;
        public MasterAdminController(IMasterAdmin iMasterAdmin)
        {
            _IMasterAdmin = iMasterAdmin;
        }
        public async Task<IActionResult> Index()
        {
            var admins = await _IMasterAdmin.Get();
            return View(admins);
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile(int Id)
        {
            var admins = await _IMasterAdmin.Find(Id);
            return View(admins);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(MasterAdminModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            await _IMasterAdmin.UpdateProfile(data);
            return RedirectToAction("Index");
        }

    }
}
