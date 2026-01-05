using KandangMobil.Filters;
using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Master;

namespace KandangMobil.Controllers.User
{
    [UserAuthorize]
    public class UserRentalController : Controller
    {
        private readonly IMasterRental _IMasterRental;
        private readonly IMasterKendaraan _IMasterKendaraan;
        private readonly IMasterUser _IMasterUser;
        public UserRentalController(IMasterRental iMasterRental, IMasterKendaraan iMasterKendaraan, IMasterUser iMasterUser)
        {
            _IMasterRental = iMasterRental;
            _IMasterKendaraan = iMasterKendaraan;
            _IMasterUser = iMasterUser;
        }
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "AuthUser");

            var rentals = await _IMasterRental.FindByUser(userId.Value);
            foreach (var r in rentals)
            {
                r.Kendaraan = await _IMasterKendaraan.Find(r.IdKendaraan);
                r.User = await _IMasterUser.Find(r.UserId);
            }
            return View(rentals);
        }
        public async Task<IActionResult> Add()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "AuthUser");

            var user = await _IMasterUser.Find(userId.Value);
            ViewBag.CarList = await _IMasterKendaraan.Get();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MasterRentalModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "AuthUser");
            data.UserId = userId.Value;

            await _IMasterRental.Add(data);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            ViewBag.CarList = await _IMasterKendaraan.Get();
            ViewBag.UserList = await _IMasterUser.Get();
            var rentals = await _IMasterRental.Find(Id);
            return View(rentals);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MasterRentalModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "AuthUser");
            data.UserId = userId.Value;

            await _IMasterRental.Update(data);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var rentals = await _IMasterRental.Find(Id);
            if (rentals != null)
            {
                await _IMasterRental.Remove(rentals);
            }
            return RedirectToAction("Index");
        }
    }
}
