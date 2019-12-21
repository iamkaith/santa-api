using Assignment2.Models;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.ViewModels {
    public class AdminEditUser
    {

        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        [MaxLength(2)]
        public string Province { get; set; }
        [MaxLength(7)]
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsNaugthy { get; set; }

        /*
         * Transform the ApplicationUser to this model
         * Intent is to limit data sent back to client
         * Only difference is admin visibility to IsNaughty property and visibility to ids
         */
        public static AdminEditUser Transform(ApplicationUser user) {
            AdminEditUser temp = new AdminEditUser {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate.ToString(),
                Street = user.Street,
                City = user.City,
                Province = user.Province, 
                PostalCode = user.PostalCode,
                Country = user.Country,
                Latitude = user.Latitude,
                Longitude = user.Longitude,
                IsNaugthy = user.IsNaugthy
            };

            return temp;
        }
    }
}