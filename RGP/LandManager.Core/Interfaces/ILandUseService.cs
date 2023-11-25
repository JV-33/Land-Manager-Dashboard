using LandManager.Models;

namespace LandManager.Core
{
    public interface ILandUseService
    {
        Task<LandUse> GetByIdAsync(int id);
        Task<IEnumerable<LandUse>> GetAllAsync();
        Task AddAsync(LandUse landUse);
        Task UpdateAsync(LandUse landUse);
        Task DeleteAsync(int id);
    }
}