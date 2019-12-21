using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.ViewModels {
    public class Register {

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string BirthDate { get; set; }
        
        public string Street { get; set; }
        
        public string City { get; set; }

        public string Province { get; set; }

        public string PostalCode { get; set; }
        
        public string Country { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}