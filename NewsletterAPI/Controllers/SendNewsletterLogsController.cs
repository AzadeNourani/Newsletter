using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsletterAPI.Data.Contexts;
using NewsletterAPI.Data.Models;

namespace NewsletterAPI.Controllers
{
    public class SendNewsletterLogsController : Controller
    {
        private readonly ISendNewsToPersonnelListService _sendNewsToPersonnelListService;

        public SendNewsletterLogsController(ISendNewsToPersonnelListService sendNewsToPersonnelListService)
        {
            _sendNewsToPersonnelListService = sendNewsToPersonnelListService;
        }

        [HttpPost("News-log")]
        public async Task<IActionResult> Index()
        {
            BackgroundJob.Enqueue <ISendNewsToPersonnelListService>(p => p.ExecuteAsync());
           //await _sendNewsToPersonnelListService.ExecuteAsync();
            return Ok("lastNews Sent to All prsonnels successfully");

        }

    }
}
