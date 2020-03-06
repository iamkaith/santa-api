using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Assignment2.Data;
using Assignment2.Models;
using Assignment2.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Assignment2.Controllers
{
    [Route("[controller]/[action]")]
    [EnableCors("SantaListPolicy")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ILogger<AuthController> _logger;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration, ILogger<AuthController> logger, SignInManager<ApplicationUser> signInManager) {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _signInManager = signInManager;
        }

        // POST: /Auth/Register
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] Register model) {

            string message;

            // Check if request body passes validation
            if (ModelState.IsValid) {
                _logger.LogDebug("REGISTER USER: model state is valid");
            }
            else {
                _logger.LogError("REGISTER USER: modelstate is invalid");
                message = "Sorry! We are unable to register you right now. Please double-check your information to ensure it is correct.";

                return BadRequest( new { message });
            }

            ApplicationUser user = new ApplicationUser {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = DateTime.Parse(model.BirthDate),
                Street = model.Street,
                City = model.City,
                Province = model.Province,
                PostalCode = model.PostalCode,
                Country = model.Country,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Email = model.Email,
                UserName = model.FirstName,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var check = await _userManager.FindByNameAsync(user.Email);
            if(check == null) {
                _logger.LogWarning($"USER REGISTRATION: {check.Email} already exists. Must be a unique email address.");
                message = "Sorry! We are unable to register you right now. Please try again later.";

                return BadRequest(new { message});
            } 

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded && check == null) {
                await _userManager.AddPasswordAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, ApplicationRole.CHILD);

                _logger.LogDebug($"CREATE USER: User registered: { user.Email } \n");
                message = "Thank you! You have successfully registered"; 

                return Ok(new {message});
            } else {
                _logger.LogWarning($"CREATE USER: Unable to register user: { user.Email } \n");
                 message = "Sorry! Unable to register you right now.";

                return Unauthorized( new { message });
            }
        }

        // POST: /Auth/Login
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] Login model) {
            string message;
            
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password)) {
                var roles = await _userManager.GetRolesAsync(user);
                _logger.LogDebug($"Role assigned to { user.Email } + { roles[0] } ");

                // mapped role and user email to claims
                var claim = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(ClaimTypes.Role, roles[0])
                };
                _logger.LogWarning("CLAIM CREATED FOR: " + user.Email);
                var signinKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

                int expiryInMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

                var token = new JwtSecurityToken(
                  issuer: _configuration["Jwt:Site"],
                  audience: _configuration["Jwt:Site"],
                  expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                  claims: claim,
                  signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );

                _logger.LogDebug($"Token created for {model.Email}");
                message = "Success! You are able to login now."; 

                return Ok( // return status code 200 and a token
                  new {
                      token = new JwtSecurityTokenHandler().WriteToken(token),
                      expiration = token.ValidTo,
                      role = roles[0],
                      message = message 
                  });
            }
            else {
                _logger.LogError($"Token was not created for { model.Email }");
            }

            message = "Incorrect email or password combination.";
            return Unauthorized(new { message });
        }

        // POST: /Auth/Logout
        [HttpPost]
        public async Task<ActionResult> Logout() {
            string message = "Thank you! Logout successful. See you again soon.";
            await _signInManager.SignOutAsync();

            return Ok( new { message });
        }
    }
}