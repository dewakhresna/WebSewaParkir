using KandangMobil.Filters;
using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Master;

namespace KandangMobil.Controllers.Master
{
    [AdminAuthorize]
    public class MasterParkirSlotController : Controller
    {
        private readonly IMasterParkirSlot _IMasterParkirSlot;

        public MasterParkirSlotController(IMasterParkirSlot iMasterParkirSlot)
        {
            _IMasterParkirSlot = iMasterParkirSlot;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _IMasterParkirSlot.Get();
            return View(products);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MasterParkirSlotModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            await _IMasterParkirSlot.Add(data);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var products = await _IMasterParkirSlot.Find(id);
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MasterParkirSlotModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            await _IMasterParkirSlot.Update(data);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var products = await _IMasterParkirSlot.Find(id);
            if (products != null)
            {
                await _IMasterParkirSlot.Remove(products);
            }
            return RedirectToAction("Index");
        }
    }
}
