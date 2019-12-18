using Assignment2.Models;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.ViewModels {
    public class ChildEditUser 
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
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
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        /*
         * Transform the ApplicationUser to this model
         * Intent is to limit data sent back to client
         */
        public static ChildEditUser Transform(ApplicationUser user) {
            ChildEditUser temp = new ChildEditUser {
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
            };

            return temp;
        }
    }
}