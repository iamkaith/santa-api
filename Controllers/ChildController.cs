using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Assignment2.Data;
using Assignment2.Models;
using Assignment2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Assignment2.Controllers {

    [Authorize]
    [Route("[controller]/[action]")]
    [EnableCors("SantaListPolicy")]
    public class ChildController : ControllerBase {        
        private readonly IConfiguration _configuration;
        
        private readonly UserManager<ApplicationUser> _userManager;
        private ILogger<ChildController> _logger;
        private ApplicationDbContext _context;

        public ChildController(UserManager<ApplicationUser> userManager, IConfiguration configuration, ILogger<ChildController> logger, ApplicationDbContext context) {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Profile() {

            string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            if(user != null) {
                ChildEditUser temp = ChildEditUser.Transform(user); 
                return Ok(temp);

            } else {
                return NotFound();
            }
        } 

        [HttpPut]
        public async Task<ActionResult> Profile([FromBody] ChildEditUser model) {

            if (!ModelState.IsValid) {
                _logger.LogError("ERROR: update profile model state is invalid");
            }

            string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var user = await _userManager.FindByEmailAsync(email);
            if(user != null) {
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.BirthDate = DateTime.Parse(model.BirthDate);
                user.Street = model.Street;
                user.City = model.City;
                user.Province = model.Province;
                user.PostalCode = model.PostalCode;
                user.Country = model.Country;
                user.Latitude = model.Latitude;
                user.Longitude = model.Longitude;
                user.UserName = model.FirstName.ToLower();

                var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded) {
                    return Ok();

                } else {
                    return BadRequest(); 
                }
                
            } else {
               return NotFound();
            }
        }
    } 
}