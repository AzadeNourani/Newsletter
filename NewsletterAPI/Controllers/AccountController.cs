using NewsletterAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsletterAPI.DTOs;

namespace NewsletterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AccountController : ControllerBase
    {

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public IActionResult Login([FromForm] UserDTO user)
        {
            var jwtAuthorizationManager = new JWTAuthorizationManager();
            var result = jwtAuthorizationManager.Authenticate(user.EmailAddress, user.Password);
            if (result == null)
                return Unauthorized();
            else
                return Ok(result); // Return the JWT token
        }
        
        [HttpGet]
        [Authorize]
        [Route("CheckLogin")]
        public IActionResult CheckLogin()
        {
            // Authenticate the user and generate a JWT token
            return Ok("You are Authorized");
        }
    }
}