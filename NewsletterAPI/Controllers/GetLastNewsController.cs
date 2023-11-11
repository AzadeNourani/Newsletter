using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsletterAPI.Data.Contexts;
using NewsletterAPI.Data.Services.Queries.GetLastNews;
using NewsletterAPI.Dto;

namespace NewsletterAPI.Controllers
{
    public class GetLastNewsController : Controller
    {
        //private readonly NewsDbContext _newsDbContext;

        //public GetLastNewsController(NewsDbContext newsDbContext)
        //{
        //    _newsDbContext = newsDbContext;
        //}


        private readonly IGetLastNewsService _getlastNewsService;
        public GetLastNewsController(IGetLastNewsService getlastNewsService)
        {
            _getlastNewsService = getlastNewsService;
        }

        [HttpGet("get-last-news")]
        public async Task<IActionResult> GetLastNews()
        {
            try
            {
                await _getlastNewsService.ExecuteAsync();

                return Ok("News sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
