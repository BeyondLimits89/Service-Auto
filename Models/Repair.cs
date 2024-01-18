namespace Service_Auto.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Repair
    {
        [Key]
        public int ServiceID { get; set; }

        [Required, MaxLength(500)]
        [Display(Name = "Descriere serviciu")]
        public string Description { get; set; }
        [Display(Name = "Durata serviciu")]
        public int Time { get; set; } // Consider representing time differently if needed

        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Pret")]
        public decimal Cost { get; set; }

        [ForeignKey("Vehicle")]
        [Display(Name = "Vehicul")]
        public int VehicleID { get; set; }
        [Display(Name = "Vehicul")]
        public Vehicle? Vehicle { get; set; }
    }
}
