using LandManager.Models;
using Microsoft.EntityFrameworkCore;
using LandManager.Data;
using LandManager.Core;

namespace LandManager.Service
{
    public class LandUseService : ILandUseService
    {
        private readonly LandManagerContext _context;

        public LandUseService(LandManagerContext context)
        {
            _context = context;
        }

        public async Task<LandUse> GetByIdAsync(int id)
        {
            return await _context.LandUses.FirstOrDefaultAsync(l => l.LandUseId == id);
        }

        public async Task<IEnumerable<LandUse>> GetAllAsync()
        {
            return await _context.LandUses.ToListAsync();
        }

        public async Task AddAsync(LandUse landUse)
        {
            _context.LandUses.Add(landUse);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LandUse landUse)
        {
            _context.Entry(landUse).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var landUse = await _context.LandUses.FindAsync(id);
            if (landUse != null)
            {
                _context.LandUses.Remove(landUse);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}