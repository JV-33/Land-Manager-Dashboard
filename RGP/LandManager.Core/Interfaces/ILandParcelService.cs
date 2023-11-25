using LandManager.Models;

namespace LandManager.Core
{
    public interface ILandParcelService
    {
        Task<LandParcel> GetByIdAsync(int id);
        Task<IEnumerable<LandParcel>> GetAllAsync();
        Task AddAsync(LandParcel landParcel);
        Task UpdateAsync(LandParcel landParcel);
        Task DeleteAsync(int id);
    }
}