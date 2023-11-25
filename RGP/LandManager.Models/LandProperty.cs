using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LandManager.Models
{
    public class LandProperty
    {
        [Key]
        public int LandPropertyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "Kadastra numuram jābūt 11 zīmēm garumā.")]
        public string CadastreNumber { get; set; } 

        [Required]
        public string Status { get; set; } 

        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public Person Owner { get; set; }

        public ICollection<LandParcel> LandParcels { get; set; }
    }
}