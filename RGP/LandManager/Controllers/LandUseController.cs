using Microsoft.AspNetCore.Mvc;
using LandManager.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LandManager.Core;

namespace LandManager.Controllers
{
    public class LandUseController : Controller
    {
        private readonly ILandUseService _landUseService;

        public LandUseController(ILandUseService landUseService)
        {
            _landUseService = landUseService;
        }

        public async Task<IActionResult> Index()
        {
            var landUses = await _landUseService.GetAllAsync();
            return View("~/Views/Home/LandUsageList.cshtml", landUses);
        }

        // GET: /LandUse/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /LandUse/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,AreaInHectares,LandParcelId")] LandUse landUse)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _landUseService.AddAsync(landUse);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                // Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Neizdevās saglabāt izmaiņas. " +
                                             "Mēģiniet vēlreiz, un, ja problēma atkārtojas, " +
                                             "lūdzu, sazinieties ar sistēmas administratoru.");
            }

            return View(landUse);
        }

        // GET: /LandUse/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var landUse = await _landUseService.GetByIdAsync(id);
            if (landUse == null)
            {
                return NotFound();
            }
            return View(landUse);
        }

        // POST: /LandUse/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LandUseId,Type,AreaInHectares,LandParcelId")] LandUse landUse)
        {
            if (id != landUse.LandUseId)
            {
                return NotFound();
            }

            try
            {
                await _landUseService.UpdateAsync(landUse);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                // Log the error and display a relevant message to the user
            }

            return View(landUse);
        }

        // GET: /LandUse/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var landUse = await _landUseService.GetByIdAsync(id);
            if (landUse == null)
            {
                return NotFound();
            }
            return View(landUse);
        }

        // POST: /LandUse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _landUseService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: /LandUse/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var landUse = await _landUseService.GetByIdAsync(id.Value);
            if (landUse == null)
            {
                return NotFound();
            }

            return View(landUse);
        }
    }
}