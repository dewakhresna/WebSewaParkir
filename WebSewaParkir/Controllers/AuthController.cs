using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebSewaParkir.Data;
using WebSewaParkir.Models;

namespace WebSewaParkir.Controllers
{
    public class AuthController : Controller
    {
        private SewaParkirContext db = new SewaParkirContext();

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["AdminId"] != null)
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            System.Diagnostics.Debug.WriteLine("Email: " + email);
            System.Diagnostics.Debug.WriteLine("Password: " + password);

            var admin = db.Admins.FirstOrDefault(a => a.Email == email && a.Password == password);

            if (admin != null)
            {
                // Simpan status login di session
                Session["AdminLogin"] = true;
                Session["AdminId"] = admin.Id;

                return RedirectToAction("Dashboard", "Admin");
            }

            ViewBag.Error = "Username atau password salah!";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
