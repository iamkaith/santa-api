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

        // Postal code regex: https://www.oreilly.com/library/view/regular-expressions-cookbook/9781449327453/ch04s15.html
        [MaxLength(7), RegularExpression("^(?!.*[DFIOQU])[A-VXY][0-9][A-Z]●?[0-9][A-Z][0-9]$")]
        public string PostalCode { get; set; }
        
        public string Country { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public bool IsNaugthy { get; set; }
    }
}