using LandManager.Core;
using LandManager.Data;
using LandManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LandManager.Service
{
    public class PersonService : IPersonService
    {
        private readonly LandManagerContext _context;

        public PersonService(LandManagerContext context)
        {
            _context = context;
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.PersonId == id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task AddAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<SummaryViewModel> GetSummaryViewModelAsync(int personId)
        {
            var person = await _context.Persons
                                       .Include(p => p.LandProperties)
                                       .ThenInclude(lp => lp.LandParcels)
                                       .ThenInclude(l => l.LandUses)
                                       .FirstOrDefaultAsync(p => p.PersonId == personId);

            if (person == null)
            {
                return null;
            }

            var viewModel = new SummaryViewModel
            {
                Persons = new List<Person> { person },
                LandProperties = person.LandProperties.ToList(),
                LandParcels = person.LandProperties.SelectMany(lp => lp.LandParcels).ToList(),
                LandUses = person.LandProperties.SelectMany(lp => lp.LandParcels).SelectMany(l => l.LandUses).ToList()
            };

            return viewModel;
        }

        public async Task<IEnumerable<Person>> SearchAsync(string searchString)
        {
            var query = _context.Persons.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.FirstName.Contains(searchString) ||
                                         p.LastName.Contains(searchString) ||
                                         p.PersonalCodeOrRegistrationNumber.Contains(searchString) ||
                                         p.Title.Contains(searchString) ||
                                         p.Type.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}