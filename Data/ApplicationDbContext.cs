using Assignment2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Assignment2.Data { 
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            :base(options) {}
            
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }
    }
}