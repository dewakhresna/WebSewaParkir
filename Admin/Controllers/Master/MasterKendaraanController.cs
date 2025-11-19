using KandangMobil.Filters;
using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Master;

namespace KandangMobil.Controllers.Master
{
    [AdminAuthorize]
    public class MasterKendaraanController : Controller
    {
        private readonly IMasterKendaraan _IMasterKendaraan;

        public MasterKendaraanController(IMasterKendaraan iMasterKendaraan)
        {
            _IMasterKendaraan = iMasterKendaraan;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _IMasterKendaraan.Get();
            return View(products);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MasterKendaraanModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            await _IMasterKendaraan.Add(data);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var products = await _IMasterKendaraan.Find(id);
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MasterKendaraanModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            await _IMasterKendaraan.Update(data);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var products = await _IMasterKendaraan.Find(id);
            if (products != null)
            {
                await _IMasterKendaraan.Remove(products);
            }
            return RedirectToAction("Index");
        }
    }
}
