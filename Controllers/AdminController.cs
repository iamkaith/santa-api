using System;
using System.Collections.Generic;
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
    public class AdminController : ControllerBase {        
        private readonly IConfiguration _configuration;
        
        private readonly UserManager<ApplicationUser> _userManager;
        private ILogger<AdminController> _logger;
        private ApplicationDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, IConfiguration configuration, ILogger<AdminController> logger, ApplicationDbContext context) {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }
         
        // GET: /Admin/Profile
        [HttpGet]
        public async Task<ActionResult> Profile() {

            string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            if(user != null) {
                AdminEditUser temp = AdminEditUser.Transform(user); 
                return Ok(temp);

            } else {
                return NotFound();
            }
        } 

        // PUT: /Admin/Profile
        [HttpPut]
        public async Task<ActionResult> Profile([FromBody] AdminEditUser model) {

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
                user.IsNaugthy = model.IsNaugthy;

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

        // GET: /Admin/List
        [HttpGet]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ActionResult> List() {
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

            string role = User.FindFirst(ClaimTypes.Role)?.Value;          

            if(role == ApplicationRole.ADMIN) {

                List<ApplicationUser> users = _context.Users.ToList();
                List<AdminEditUser> list = new List<AdminEditUser>();

                foreach(ApplicationUser user in users) {
                    AdminEditUser temp = AdminEditUser.Transform(user);
                    list.Add(temp);
                }

                return Ok( list );

            } else {
                return Unauthorized(); 
            }
        }

        // GET: /Admin/GetChild/{id}
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetChild (string id) {

            string role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role == ApplicationRole.ADMIN) {
                ApplicationUser user = await _userManager.FindByIdAsync(id);

                if (user != null) {
                    AdminEditUser temp = AdminEditUser.Transform(user);
                    return Ok(temp);

                } else {
                    return NotFound();
                }
            } else { 
                return Unauthorized();

            }
        }

        // PUT: /Admin/UpdateChild/{id}
        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult> UpdateChild (string id, [FromBody] AdminEditUser model) {
            
            if (!ModelState.IsValid) {
                _logger.LogError("ERROR: AdminController UpdateChild model state as an admin is invalid");
            }

            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role == ApplicationRole.ADMIN) {
                var user = await _userManager.FindByIdAsync(id);

                if (user != null) {
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
                    user.IsNaugthy = model.IsNaugthy;
   
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded) {
                        return Ok();
                        
                    } else {
                        return BadRequest();
                    }
                } else {
                    return NotFound();
                }
            } else {
               return Unauthorized();
            }
        }
        
        // DELETE: /Admin/DeleteChild/{id}
        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteChild (string id) {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if(role == ApplicationRole.ADMIN) {

                ApplicationUser user = await _userManager.FindByIdAsync(id);

                if(user != null) {
                    var result = await _userManager.DeleteAsync(user);
                    
                    if(result.Succeeded) {
                        return Ok();

                    } else {
                        return NotFound();
                    } 

                } else {
                    return BadRequest();
                }

            } else { 
                return Unauthorized();
            }
        }
    } 
}
