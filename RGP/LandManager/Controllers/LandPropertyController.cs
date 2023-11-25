using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LandManager.Models;
using LandManager.Service.Implementations;
using LandManager.Core;
using Microsoft.EntityFrameworkCore;

namespace LandManager.Controllers
{
    public class LandPropertyController : Controller
    {
        private readonly ILandPropertyService _landPropertyService;

        public LandPropertyController(ILandPropertyService landPropertyService)
        {
            _landPropertyService = landPropertyService;
        }

        public async Task<IActionResult> Index()
        {
            var landProperties = await _landPropertyService.GetAllAsync();
            return View("~/Views/Home/PropertyList.cshtml", landProperties);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: /LandProperty/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, CadastreNumber, Status, OwnerId")] LandProperty landProperty)
        {
            await _landPropertyService.AddAsync(landProperty);
            return RedirectToAction(nameof(Index));
        }

        // GET: /LandProperty/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var landProperty = await _landPropertyService.GetByIdAsync(id.Value);
            if (landProperty == null)
            {
                return NotFound();
            }
            return View(landProperty);
        }

        // POST: /LandProperty/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LandPropertyId, Name, CadastreNumber, Status, OwnerId")] LandProperty landProperty)
        {
            if (id != landProperty.LandPropertyId)
            {
                return NotFound();
            }
                try
                {
                    await _landPropertyService.UpdateAsync(landProperty);
                }
                catch
                {
                    if (!await LandPropertyExists(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var landProperty = await _landPropertyService.GetByIdWithLandParcelsAsync(id.Value);

            if (landProperty == null)
            {
                return NotFound();
            }

            return View(landProperty);
        }

        // GET: /LandProperty/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var landProperty = await _landPropertyService.GetByIdAsync(id.Value);
            if (landProperty == null)
            {
                return NotFound();
            }

            return View(landProperty);
        }

        // POST: /LandProperty/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _landPropertyService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LandPropertyExists(int id)
        {
            var landProperty = await _landPropertyService.GetByIdAsync(id);
            return landProperty != null;
        }
    }
}