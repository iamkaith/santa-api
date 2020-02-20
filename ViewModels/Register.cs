using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.ViewModels {
    public class Register {

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        public string BirthDate { get; set; }
        
        public string Street { get; set; }
        
        public string City { get; set; }

        [MaxLength(2)]
        public string Province { get; set; }

        [MaxLength(7), RegularExpression("^[ABCEGHJ-NPRSTVXY]{1}[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[ ]?[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[0-9]{1}$")]
        public string PostalCode { get; set; }
        
        public string Country { get; set; }

        [Required, Range(-90.0, 90.0)]
        public double Latitude { get; set; }

        [Required, Range(0.0, 180.0)]
        public double Longitude { get; set; }
    }
}