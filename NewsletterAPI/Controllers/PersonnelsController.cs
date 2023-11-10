using Microsoft.AspNetCore.Mvc;
using NewsletterAPI.Data.Services.GetPersonnelList;

namespace NewsletterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonnelsController : Controller
    {
       
        private readonly IGetPersonnelListService _getpersonnelListService;

        public PersonnelsController(IGetPersonnelListService getpersonnelListService)
        {
            _getpersonnelListService = getpersonnelListService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var personnelList = await _getpersonnelListService.ExecuteAsync();

            return personnelList != null ?
                        View(personnelList) :
                        Problem("Entity set 'NewsDbContext.Personnels' is null.");
            //return View(personnelList);
        }


    }
}
