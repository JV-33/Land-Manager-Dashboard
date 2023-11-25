namespace LandManager.Models
{
    public class SummaryViewModel
    {
        public IEnumerable<Person> Persons { get; set; }
        public IEnumerable<LandProperty> LandProperties { get; set; }
        public IEnumerable<LandParcel> LandParcels { get; set; }
        public IEnumerable<LandUse> LandUses { get; set; }
    }
}