using System.ComponentModel.DataAnnotations;

namespace LandManager.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Title { get; set; }

        [Required]
        public string PersonalCodeOrRegistrationNumber { get; set; }

        [Required]
        public string Type { get; set; }

        public ICollection<LandProperty> LandProperties { get; set; }
    }
}