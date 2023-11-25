using Microsoft.AspNetCore.Mvc;
using LandManager.Data;
using LandManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LandManager.Controllers
{
    public class DashboardController : Controller
    {
        private readonly LandManagerContext _context;

        public DashboardController(LandManagerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Summary()
        {
            var model = new SummaryViewModel
            {
                Persons = await _context.Persons.ToListAsync(),
                LandProperties = await _context.LandProperties.ToListAsync(),
                LandParcels = await _context.LandParcels.ToListAsync(),
                LandUses = await _context.LandUses.ToListAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> IncompleteData()
        {
            var personsWithoutProperties = await _context.Persons
                .Where(p => !p.LandProperties.Any())
                .ToListAsync();

            var propertiesWithoutLandParcels = await _context.LandProperties
                .Where(lp => !lp.LandParcels.Any())
                .ToListAsync();

            var landParcelsWithoutLandUses = await _context.LandParcels
                .Include(lp => lp.LandUses)
                .Where(lp => !lp.LandUses.Any())
                .ToListAsync();

            var model = new IncompleteDataViewModel
            {
                PersonsWithoutProperties = personsWithoutProperties,
                PropertiesWithoutLandParcels = propertiesWithoutLandParcels,
                LandParcelsWithoutLandUses = landParcelsWithoutLandUses
            };

            return View(model);
        }
    }
}