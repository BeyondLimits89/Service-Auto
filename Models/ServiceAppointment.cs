namespace Service_Auto.Models

{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ServiceAppointment
    {
        [Key]
        public int AppointmentID { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleID { get; set; }
        [Display(Name = "Vehicul - Proprietar")]
        public Vehicle? Vehicle { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data programare")]
        public DateTime Date { get; set; }

        [NotMapped]
        public string SelectedTime { get; set; }

        [Display(Name = "Ora Programare")]
        public TimeSpan Time
        {
            get => TimeSpan.Parse(SelectedTime);
            set => SelectedTime = value.ToString(@"hh\:mm");
        }

        [Required]
        [Display(Name = "Status programare")]
        public AppointmentStatus Status { get; set; }
    }

    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Cancelled
    }
    
}
