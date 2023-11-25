using Microsoft.AspNetCore.Mvc;
using LandManager.Models;
using LandManager.Core;

namespace LandManager.Controllers
{
    public class LandParcelController : Controller
    {
        private readonly ILandParcelService _landParcelService;

        public LandParcelController(ILandParcelService landParcelService)
        {
            _landParcelService = landParcelService;
        }

        public async Task<IActionResult> Index()
        {
            var landParcels = await _landParcelService.GetAllAsync();
            return View("~/Views/Home/LandParcelList.cshtml", landParcels);
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: /LandParcel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TotalAreaInHectares, SurveyDate, LandPropertyId")] LandParcel landParcel)
        {
            await _landParcelService.AddAsync(landParcel);
            return RedirectToAction(nameof(Index));
        }

        // GET: /LandParcel/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var landParcel = await _landParcelService.GetByIdAsync(id.Value);
            if (landParcel == null)
            {
                return NotFound();
            }
            return View(landParcel);
        }

        // POST: /LandParcel/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LandParcelId, TotalAreaInHectares, SurveyDate, LandPropertyId")] LandParcel landParcel)
        {
            if (id != landParcel.LandParcelId)
            {
                return NotFound();
            }

            {
                try
                {
                    await _landParcelService.UpdateAsync(landParcel);
                }
                catch
                {
                    if (!await LandParcelExists(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: /LandParcel/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var landParcel = await _landParcelService.GetByIdAsync(id.Value);
            if (landParcel == null)
            {
                return NotFound();
            }

            return View(landParcel);
        }

        // POST: /LandParcel/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _landParcelService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LandParcelExists(int id)
        {
            var landParcel = await _landParcelService.GetByIdAsync(id);
            return landParcel != null;
        }

        // GET: /LandParcel/Details/1
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var landParcel = await _landParcelService.GetByIdAsync(id.Value);
            if (landParcel == null)
            {
                return NotFound();
            }

            return View(landParcel);
        }
    }
}