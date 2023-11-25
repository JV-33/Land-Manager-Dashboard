using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LandManager.Models
{
    public class LandParcel
    {
        [Key]
        public int LandParcelId { get; set; }

        [Required]
        public double TotalAreaInHectares { get; set; }

        [Required]
        public DateTime SurveyDate { get; set; }

        [ForeignKey("LandProperty")]
        public int LandPropertyId { get; set; }
        public LandProperty LandProperty { get; set; }


        public ICollection<LandUse> LandUses { get; set; }
    }
}