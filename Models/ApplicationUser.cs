using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Assignment2.Models {
    public class ApplicationUser : IdentityUser {

        public ApplicationUser() : base() {}

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public string Street { get; set; }
        
        public string City { get; set; }

        [MaxLength(2)]
        public string Province { get; set; }

        [MaxLength(7)]
        public string PostalCode { get; set; }
        
        public string Country { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool IsNaugthy { get; set; }

        public DateTime DateCreated { get; set; }
    }
}