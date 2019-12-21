using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment2.Models;
using Microsoft.AspNetCore.Identity;

namespace Assignment2.Data { 
   public class SeedData {
       public static async Task Initialize  (ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) {
           context.Database.EnsureCreated(); 

            string[] roles = {ApplicationRole.ADMIN, ApplicationRole.CHILD};
            string password = "P@$$w0rd";  

            Dictionary<string, string> users = new Dictionary<string, string>();
            users.Add("Santa", "santa@np.com");
            users.Add("Tim", "tim@np.com");

            foreach(var role in roles) {
                if(await roleManager.FindByNameAsync(role) == null) {
                    await roleManager.CreateAsync(new ApplicationRole(role));
                }
            }

            if(await userManager.FindByEmailAsync("santa@np.com") == null ) {
                var user = new ApplicationUser {
                    UserName = "santa",
                    Email = "santa@np.com", 
                    FirstName = "Santa",
                    LastName = "Claus",
                    Street = "555 Seymour St",
                    City = "Vancouver",
                    Province = "BC",
                    PostalCode = "V6B 3H6",
                    Country = "Canada",
                    Latitude = "49.284193",
                    Longitude = "-123.115276",
                    IsNaugthy = false,
                    BirthDate = new DateTime(1970, 10, 12)
                };
                
                var result = await userManager.CreateAsync(user);
                if(result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, ApplicationRole.ADMIN);
                } 
            } 
            
            if(await userManager.FindByEmailAsync("tim@np.com") == null ) {
                var user = new ApplicationUser {
                    UserName = "tim",
                    Email = "tim@np.com", 
                    FirstName = "Tim",
                    LastName = "Claus",
                    Street = "3700 Willingdon Avenue",
                    City = "Burnaby",
                    Province = "BC",
                    PostalCode = "V5G 3H2",
                    Country = "Canada",
                    Latitude = "49.250150",
                    Longitude = "-123.000664",
                    IsNaugthy = true,
                    BirthDate = new DateTime(1990, 9, 28)
                };
                
                var result = await userManager.CreateAsync(user);
                if(result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, ApplicationRole.CHILD);
                }
            }
        } 
    }
}