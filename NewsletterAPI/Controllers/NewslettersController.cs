using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsletterAPI.Data.Contexts;
using NewsletterAPI.Data.Models;
using NewsletterAPI.DTOs;

namespace NewsletterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewslettersController : Controller
    {
        private readonly ISendNewsToPersonnelListService _sendNewsToPersonnelListService;
        private readonly NewsDbContext _context;

        public NewslettersController(ISendNewsToPersonnelListService sendNewsToPersonnelListService,NewsDbContext newsDbContext)
        {
            _sendNewsToPersonnelListService = sendNewsToPersonnelListService;
            _context = newsDbContext;
            
        }

        [HttpPost("send-test-news")]
        public async Task<IActionResult> SendNewsToPersonnels([FromBody] NewsletterDTO News)
        {
            try
            {
                if (News.Title == null || string.IsNullOrWhiteSpace(News.Title))
                {
                    return BadRequest("Invalid input. News content is required.");
                }

               // await _sendNewsToPersonnelListService.SendNewsToPersonnelList(NewsTitle.NewsTitle);
                await _context.Newsletter.AddAsync(new Newsletter
                {
                    Title=News.Title,
                    Content=News.Content,
                    SendDate=DateTime.UtcNow,
                    
                }
                    );
                await _context.SaveChangesAsync();

                return Ok("News Sent");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
