using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsletterAPI.Data.Contexts;
using NewsletterAPI.Data.Models;

namespace NewsletterAPI.Controllers
{
    public class SendNewsletterLogsController : Controller
    {
        private readonly NewsDbContext _newsDbContext;

        public SendNewsletterLogsController(NewsDbContext newsDbContext)
        {
            _newsDbContext = newsDbContext;
        }

        // GET: SendNewsletterLogs
        [HttpGet("News-log")]
        //  public async Task<IActionResult> Index()
        public IActionResult GetNewsLog()
        {
            var newsDbContext = _newsDbContext.SendNewsletterLogs.Include(s => s.Newsletter).Include(s => s.Personnel);
            return View( newsDbContext.ToList());
        }

    }
}
