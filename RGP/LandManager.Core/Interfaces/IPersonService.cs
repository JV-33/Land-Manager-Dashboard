using LandManager.Models;

namespace LandManager.Core
{
    public interface IPersonService
    {
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task<IEnumerable<Person>> SearchAsync(string searchString);
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(int id);
        Task<SummaryViewModel> GetSummaryViewModelAsync(int personId);
    }
}