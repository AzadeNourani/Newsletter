using Hangfire;
using Microsoft.EntityFrameworkCore;
using NewsletterAPI.Data.Contexts;
using NewsletterAPI.Data.Models;
using NewsletterAPI.Data.Services.Queries.GetPersonnelList;
using static ISendNewsToPersonnelListService;

public class SendNewsToPersonnelListService:ISendNewsToPersonnelListService
{
    //private readonly IBackgroundJobClient _backgroundJobClient;
    //public NewsletterService(IBackgroundJobClient backgroundJobClient)
    //{
    //    _backgroundJobClient = backgroundJobClient;

        private readonly IGetPersonnelListService _getPersonnelListService;
        private readonly NewsDbContext _context;

        public SendNewsToPersonnelListService(
            IGetPersonnelListService getPersonnelListService,
            NewsDbContext context
        )
        {
            _getPersonnelListService = getPersonnelListService;
            _context = context;
        }

        public async Task SendNewsToPersonnelList(string foo)
         {
            try
            {
                //foo = "Test News Content";
                var personnelList = await _getPersonnelListService.ExecuteAsync();

                foreach (var personnel in personnelList)
                {
                    // Send News logic goes here if needed

                    // Record in SendNewsLog
                    _context.SendNewsletterLogs.Add(new SendNewsletterLog
                    {
                        PersonnelId = personnel.Id,
                        SendTime = DateTime.UtcNow,
                        SendStatus = SendStatus.Sent,
                        NewsletterId=2,
                        NewsTitle=foo
                        // Other fields related to sending news
                    });
                }

                // Save all changes after the loop
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }


