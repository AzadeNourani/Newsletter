using Microsoft.AspNetCore.Mvc;

namespace NewsletterAPI.Controllers
{
    public class HangfireController : Controller
    {
        private readonly SendNewsletterJob _sendnewsletterJob;
        public HangfireController(SendNewsletterJob sendnewsletterJob)
        {
            _sendnewsletterJob = sendnewsletterJob;
        }

        [HttpPost("run-newsletter-job")]
        public IActionResult RunNewsletterJob()
        {
            // Manually run the SendNewsletters job
          //  _sendnewsletterJob.SendNewsletters();

            return Ok("Newsletter job executed manually.");
        }
    }
}
