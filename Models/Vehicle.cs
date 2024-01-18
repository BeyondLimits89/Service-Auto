using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace Service_Auto.Models
{
public class Vehicle
    {
        [Key]
        public int VehicleID { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Numar inmatriculare")]
        public string LicensePlate { get; set; }

        [Required]
        [Display(Name = "Brand")]
        public string Make { get; set; }

        [Required]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "An fabricatie")]
        public int Year { get; set; }

        [ForeignKey("Customer")]
        [Display(Name = "Proprietar")]
        public int CustomerID { get; set; }
        [Display(Name = "Proprietar")]
        public Customer? Customer { get; set; }

        public ICollection<Repair>? Repairs { get; set; }
    }
}
