using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LandManager.Models
{
    public class LandUse
    {
        [Key]
        public int LandUseId { get; set; }

        [Required]
        public string Type { get; set; } 

        [Required]
        public double AreaInHectares { get; set; }

        [ForeignKey("LandParcel")]
        public int LandParcelId { get; set; }
        public LandParcel LandParcel { get; set; }
    }
}