using KandangMobil.Filters;
using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Master;

namespace KandangMobil.Controllers.Master
{
    [AdminAuthorize]
    public class MasterRentalController : Controller
    {
        private readonly IMasterRental _IMasterRental;
        private readonly IMasterKendaraan _IMasterKendaraan;
        public readonly IMasterUser _IMasterUser;
        public MasterRentalController(IMasterRental iMasterRental, IMasterKendaraan iMasterKendaraan, IMasterUser iMasterUser)
        {
            _IMasterRental = iMasterRental;
            _IMasterKendaraan = iMasterKendaraan;
            _IMasterUser = iMasterUser;
        }
        public async Task<IActionResult> Index()
        {
            var rentals = await _IMasterRental.Get();
            var result = new List<CarViewModel>();

            foreach (var r in rentals)
            {
                result.Add(new CarViewModel
                {
                    Rental = r,
                    User = await _IMasterUser.Find(r.UserId),
                    Kendaraan = await _IMasterKendaraan.Find(r.IdKendaraan)
                });
            }

            return View(result);
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.UserList = await _IMasterUser.Get();
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
