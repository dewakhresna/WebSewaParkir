using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Master;

namespace KandangMobil.Controllers.Master
{
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

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MasterKendaraanModel data)
        {
            await _IMasterKendaraan.Add(data);
            return View();
        }
    }
}
