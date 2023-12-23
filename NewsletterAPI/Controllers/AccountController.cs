using NewsletterAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsletterAPI.DTOs;
using NewsletterAPI.Data.Contexts;

namespace NewsletterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AccountController : ControllerBase
    {
        private readonly NewsDbContext _dbContext;

        public AccountController(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]

        public IActionResult Login([FromForm] UserDTO user)
        {
            var dbUser = _dbContext.Users.FirstOrDefault(u => u.EmailAddress == user.EmailAddress && u.Password==user.Password);
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