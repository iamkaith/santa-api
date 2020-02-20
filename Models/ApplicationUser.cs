using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Assignment2.Models {
    public class ApplicationUser : IdentityUser {

        public ApplicationUser() : base() {}
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public string Street { get; set; }
        
        public string City { get; set; }

        [MaxLength(2)]
        public string Province { get; set; }

        // Postal code regex: https://blog.platformular.com/2012/03/03/how-to-validate-canada-postal-code-in-c/
        [MaxLength(7), RegularExpression("^[ABCEGHJ-NPRSTVXY]{1}[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[ ]?[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[0-9]{1}$")]
        public string PostalCode { get; set; }
        
        public string Country { get; set; }
        
        [Required, Range(-90.0, 90.0)]
        public double Latitude { get; set; }
        
        [Required, Range(0.0, 180.0)]
        public double Longitude { get; set; }
        public bool IsNaugthy { get; set; }
    }
}