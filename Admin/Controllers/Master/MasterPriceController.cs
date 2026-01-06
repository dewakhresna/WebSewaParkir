using KandangMobil.Filters;
using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Master;
using System.Reflection;

namespace KandangMobil.Controllers.Master
{
    [AdminAuthorize]
    public class MasterPriceController : Controller
    {
        private readonly IMasterPrice _IMasterPrice;
        public MasterPriceController(IMasterPrice iMasterPrice)
        {
            _IMasterPrice = iMasterPrice;
        }
        public async Task<IActionResult> Index()
        {
            var price = await _IMasterPrice.Get();
            return View(price);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MasterPriceModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            await _IMasterPrice.Add(data);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var rentals = await _IMasterPrice.Find(Id);
            return View(rentals);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MasterPriceModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            await _IMasterPrice.Update(data);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var rentals = await _IMasterPrice.Find(Id);
            if (rentals != null)
            {
                await _IMasterPrice.Remove(rentals);
            }
            return RedirectToAction("Index");
        }

    }
}
