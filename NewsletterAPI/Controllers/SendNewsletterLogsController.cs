using Hangfire;
using Hangfire.Common;
//using Hangfire.States;
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
        private readonly IRecurringJobManager _recurringJobManager;

        public SendNewsletterLogsController(ISendNewsToPersonnelListService sendNewsToPersonnelListService , IRecurringJobManager recurringJobManager)
        {
           _sendNewsToPersonnelListService = sendNewsToPersonnelListService;
           _recurringJobManager = recurringJobManager;
        }

        [HttpPost("News-log")]
        public async Task<IActionResult> Index()
        {

            //BackgroundJob.Enqueue <ISendNewsToPersonnelListService>(p => p.ExecuteAsync());
            //RecurringJob.AddOrUpdate<ISendNewsToPersonnelListService>("SendNewsToPersonnelList",
            //service => service.ExecuteAsync(), "*/5 * * * *");// run every five minutes
            
            _recurringJobManager.AddOrUpdate("SendNewsToPersonnels_Every_Minute", () => _sendNewsToPersonnelListService.ExecuteAsync(), Cron.Minutely);

            return Ok("lastNews Sent to All prsonnels successfully");

        }

        private object SendNewsletterJob()
        {
            throw new NotImplementedException();
        }
    }
}
