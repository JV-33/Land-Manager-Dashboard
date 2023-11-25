using LandManager.Models;

namespace LandManager.Core
{
    public interface ILandPropertyService
    {
        Task<LandProperty> GetByIdAsync(int id);
        Task<IEnumerable<LandProperty>> GetAllAsync();
        Task AddAsync(LandProperty landProperty);
        Task UpdateAsync(LandProperty landProperty);
        Task DeleteAsync(int id);

        Task<LandProperty> GetByIdWithLandParcelsAsync(int id);
    }
}