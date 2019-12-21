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

        [MaxLength(7)]
        public string PostalCode { get; set; }
        
        public string Country { get; set; }

        /* Due to limitations with SQLite, Decimal isn't supported. To workaround this Long+Lat values will be stored as a String in the interm */
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public bool IsNaugthy { get; set; }

        public DateTime DateCreated { get; set; }
    }
}