using LandManager.Models;
using Microsoft.EntityFrameworkCore;
using LandManager.Data;
using LandManager.Core;

namespace LandManager.Service.Implementations
{
    public class LandParcelService : ILandParcelService
    {
        private readonly LandManagerContext _context;

        public LandParcelService(LandManagerContext context)
        {
            _context = context;
        }

        public async Task<LandParcel> GetByIdAsync(int id)
        {
            return await _context.LandParcels
                .Include(lp => lp.LandUses) // Pievieno šo rindiņu, lai ielādētu LandUses
                .FirstOrDefaultAsync(lp => lp.LandParcelId == id);
        }

        public async Task<IEnumerable<LandParcel>> GetAllAsync()
        {
            return await _context.LandParcels.ToListAsync();
        }

        public async Task AddAsync(LandParcel landParcel)
        {
            _context.LandParcels.Add(landParcel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LandParcel landParcel)
        {
            _context.Entry(landParcel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var landParcel = await _context.LandParcels.FindAsync(id);
            if (landParcel != null)
            {
                _context.LandParcels.Remove(landParcel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}