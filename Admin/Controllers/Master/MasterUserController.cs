using Admin.Interfaces;
using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Master;
using System.Reflection;

namespace KandangMobil.Controllers.Master
{
    public class MasterUserController : Controller
    {
        private readonly IMasterUser _IMasterUser;
        public MasterUserController(IMasterUser iMasterUser)
        {
             _IMasterUser = iMasterUser;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _IMasterUser.Get();
            return View(users);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MasterUserModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            await _IMasterUser.Add(data);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var user = await _IMasterUser.Find(Id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MasterUserModel data)
        {
            var existingUser = await _IMasterUser.Find(data.Id);
            if (existingUser == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(data.Password))
            {
                data.Password = existingUser.Password;
            }

            await _IMasterUser.Update(data);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _IMasterUser.Find(id);
            if (user != null)
            {
                await _IMasterUser.Remove(user);
            }
            return RedirectToAction("Index");
        }

    }
}
