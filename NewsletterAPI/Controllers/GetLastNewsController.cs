using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsletterAPI.Data.Contexts;
using NewsletterAPI.Data.Services.Queries.GetLastNews;
using NewsletterAPI.Dto;

namespace NewsletterAPI.Controllers
{
    public class GetLastNewsController : Controller
    {
        private readonly IGetLastNewsService _getlastNewsService;
        private readonly NewsDbContext _newsDbContext;
        public GetLastNewsController(IGetLastNewsService getlastNewsService, NewsDbContext newsDbContext)
        {
            _getlastNewsService = getlastNewsService;
            _newsDbContext = newsDbContext;
        }

        [HttpGet("get-last-news")]
        public async Task<IActionResult> GetLastNews()
        {
            try
            {
                var lastNewsletter = await _getlastNewsService.ExecuteAsync();

                 return Ok(lastNewsletter);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
