using KandangMobil.Helpers;
using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KandangMobil.Controllers.Auth
{
    public class AuthUserController : Controller
    {
        private readonly IMasterUser _IMasterUser;

        public AuthUserController(IMasterUser iMasterUser)
        {
            _IMasterUser = iMasterUser;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return RedirectToAction("Index", "UserRental");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _IMasterUser.Login(email);

            if (user == null)
            {
                ViewBag.Error = "Email tidak ditemukan!";
                return View("Index");
            }

            string hashedInputPassword = HashHelper.ToSha256(password);

            if (hashedInputPassword != user.Password)
            {
                ViewBag.Error = "Password salah!";
                return View("Index");
            }

            HttpContext.Session.SetInt32("UserId", user.Id);

            return RedirectToAction("Index", "UserRental");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
