namespace Service_Auto.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Nume Prenume")]
        public string Name { get; set; }

        [Required, MaxLength(200)]
        [Display(Name = "Contact")]
        public string ContactInformation { get; set; }

        [Required, MaxLength(200)]
        [Display(Name = "Adresa")]
        public string Address { get; set; }

        public ICollection<Vehicle>? Vehicles { get; set; }
    }
}
