using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtInCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AggregeateController : ControllerBase
    {
        [Authorize]
        [HttpPost]
        [Route("Sum")]
        public IActionResult Sum([FromQuery(Name = "Num1")]int num1, [FromQuery(Name = "Num2")] int num2)
        {
            var result = num1 + num2;
            return Ok(result);
        }
    }
}
