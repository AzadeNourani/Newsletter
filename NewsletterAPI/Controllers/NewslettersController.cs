using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsletterAPI.Data.Contexts;
using NewsletterAPI.Data.Models;

namespace NewsletterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewslettersController : Controller
    {
        private readonly NewsDbContext _context;

        public NewslettersController(NewsDbContext context)
        {
            _context = context;
        }

        // GET: Newsletters
        [HttpGet]
        public async Task<IActionResult> Index()
        {
              return _context.Newsletter != null ? 
                          View(await _context.Newsletter.ToListAsync()) :
                          Problem("Entity set 'NewsDbContext.Newsletter'  is null.");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Newsletter newsletter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsletter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsletter);
        }


        private bool NewsletterExists(int id)
        {
          return (_context.Newsletter?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
