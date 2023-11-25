using Microsoft.EntityFrameworkCore;
using LandManager.Models;

namespace LandManager.Data
{
    public class LandManagerContext : DbContext
    {
        public LandManagerContext(DbContextOptions<LandManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<LandProperty> LandProperties { get; set; }
        public DbSet<LandParcel> LandParcels { get; set; }
        public DbSet<LandUse> LandUses { get; set; }
    }
}