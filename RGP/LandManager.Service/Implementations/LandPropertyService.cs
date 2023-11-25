using LandManager.Models;
using Microsoft.EntityFrameworkCore;
using LandManager.Data;
using LandManager.Core;

namespace LandManager.Service
{
    public class LandPropertyService : ILandPropertyService
    {
        private readonly LandManagerContext _context;

        public LandPropertyService(LandManagerContext context)
        {
            _context = context;
        }

        public async Task<LandProperty> GetByIdAsync(int id)
        {
            return await _context.LandProperties.FirstOrDefaultAsync(lp => lp.LandPropertyId == id);
        }

        public async Task<IEnumerable<LandProperty>> GetAllAsync()
        {
            return await _context.LandProperties.ToListAsync();
        }

        public async Task AddAsync(LandProperty landProperty)
        {
            _context.LandProperties.Add(landProperty);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LandProperty landProperty)
        {
            _context.Entry(landProperty).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var landProperty = await _context.LandProperties.FindAsync(id);
            if (landProperty != null)
            {
                _context.LandProperties.Remove(landProperty);
                await _context.SaveChangesAsync();
            }
        }

        // Implementēta jauna metode
        public async Task<LandProperty> GetByIdWithLandParcelsAsync(int id)
        {
            return await _context.LandProperties
                                 .Include(p => p.LandParcels)
                                 .FirstOrDefaultAsync(p => p.LandPropertyId == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}