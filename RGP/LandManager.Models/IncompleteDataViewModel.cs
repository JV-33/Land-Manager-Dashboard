namespace LandManager.Models
{
    public class IncompleteDataViewModel
    {
        public IEnumerable<Person> PersonsWithoutProperties { get; set; }
        public IEnumerable<LandProperty> PropertiesWithoutLandParcels { get; set; }
        public IEnumerable<LandParcel> LandParcelsWithoutLandUses { get; set; }
    }
}