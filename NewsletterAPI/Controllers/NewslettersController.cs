using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsletterAPI.Data.Contexts;
using NewsletterAPI.Data.Models;
using NewsletterAPI.Dto;

namespace NewsletterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewslettersController : Controller
    {
        private readonly ISendNewsToPersonnelListService _sendNewsToPersonnelListService;
      

        public NewslettersController(ISendNewsToPersonnelListService sendNewsToPersonnelListService)
        {
            _sendNewsToPersonnelListService = sendNewsToPersonnelListService;
            
        }

        [HttpPost("send-test-news")]
        public async Task<IActionResult> SendNewsToPersonnels([FromBody] NewsletterDto NewsTitle)
        {
            try
            {
                if (NewsTitle == null || string.IsNullOrWhiteSpace(NewsTitle.NewsTitle))
                {
                    return BadRequest("Invalid input. News content is required.");
                }

                await _sendNewsToPersonnelListService.SendNewsToPersonnelList(NewsTitle.NewsTitle);

                return Ok("Test news sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
