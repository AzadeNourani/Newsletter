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
            //service => service.ExecuteAsync(), "*/5 * * * *");

            //var scheduledTime = new TimeSpan(21, 40, 0);
            //var scheduleOptions = new EnqueuedState { Queue = "default" };

            //RecurringJob.AddOrUpdate<ISendNewsToPersonnelListService>("SendNewsToPersonnelList",
            //    service => service.ExecuteAsync(), Cron.Daily(scheduledTime.Hours, scheduledTime.Minutes), scheduleOptions);

            // _recurringJobManager.AddOrUpdate("", (sendNewsToPersonnelListService) => Job, Cron.Daily);
            // _recurringJobManager.AddOrUpdate(_sendNewsToPersonnelListService, () => SendNewsletterJob(), Cron.Daily);

            _recurringJobManager.AddOrUpdate("SendNewsToPersonnel", () => _sendNewsToPersonnelListService.ExecuteAsync(), Cron.Minutely);

            //await _sendNewsToPersonnelListService.ExecuteAsync();
            return Ok("lastNews Sent to All prsonnels successfully");

        }

        private object SendNewsletterJob()
        {
            throw new NotImplementedException();
        }
    }
}
