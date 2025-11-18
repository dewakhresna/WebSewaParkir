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
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Add()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "AuthUser");

            var user = await _IMasterUser.Find(userId.Value);
            var model = new MasterRentalModel
            {
                CustomerName = user.Name,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date
            };

            ViewBag.CarList = await _IMasterKendaraan.Get();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(MasterRentalModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            await _IMasterRental.Add(data);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            ViewBag.CarList = await _IMasterKendaraan.Get();
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
            await _IMasterRental.Update(data);
            return RedirectToAction("Index");
        }
    }
}
