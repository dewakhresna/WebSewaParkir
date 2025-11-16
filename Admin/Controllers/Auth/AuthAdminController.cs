using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Master;

namespace KandangMobil.Controllers.Auth
{
    public class AuthAdminController : Controller
    {
        private readonly IMasterAdmin _IMasterAdmin;
        public AuthAdminController(IMasterAdmin iMasterAdmin)
        {
            _IMasterAdmin = iMasterAdmin;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("AdminId") != null)
            {
                return RedirectToAction("Index", "MasterRental");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var admin = await _IMasterAdmin.Login(email);

            if (admin == null)
            {
                ViewBag.Error = "Email tidak ditemukan!";
                return View("Index");
            }

            if (password != admin.Password)
            {
                ViewBag.Error = "Password salah!";
                return View("Index");
            }

            HttpContext.Session.SetInt32("AdminId", admin.Id);

            return RedirectToAction("Index", "MasterRental");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
