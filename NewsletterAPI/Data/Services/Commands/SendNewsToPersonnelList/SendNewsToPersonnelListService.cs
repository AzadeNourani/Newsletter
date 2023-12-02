using Hangfire;
using Microsoft.EntityFrameworkCore;
using NewsletterAPI.Data.Contexts;
using NewsletterAPI.Data.Models;
using NewsletterAPI.Data.Services.Queries.GetLastNews;
using NewsletterAPI.Data.Services.Queries.GetPersonnelList;
using static ISendNewsToPersonnelListService;

public class SendNewsToPersonnelListService : ISendNewsToPersonnelListService
{

    private readonly IGetPersonnelListService _getPersonnelListService;
    private readonly IGetLastNewsService _getLastNewsService;
    private readonly NewsDbContext _context;

    public SendNewsToPersonnelListService(
        IGetPersonnelListService getPersonnelListService,
        IGetLastNewsService getLastNewsService,
        NewsDbContext context
    )
    {
        _getPersonnelListService = getPersonnelListService;
        _getLastNewsService = getLastNewsService;
        _context = context;
    }
    
    //public object DeliveryReports { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    //public List<Personnel> GetAllPersonnels()
    //{
    //    throw new NotImplementedException();
    //}

    //public Newsletter GetNewsletterByDate(DateTime date)
    //{
    //    throw new NotImplementedException();
    //}

    //public Personnel GetPersonById(int personId)
    //{
    //    throw new NotImplementedException();
    //}

    //public void LogSentNewsletter(SendNewsletterLog report)
    //{
    //    throw new NotImplementedException();
    //}

    //public void SaveNewsletter(Newsletter newsletter)
    //{
    //    throw new NotImplementedException();
    //}

    public async Task ExecuteAsync()
    {
        try
        {
            //foo = "Test News Content";
            var personnelList = await _getPersonnelListService.ExecuteAsync();
            var News = await _getLastNewsService.ExecuteAsync();
            foreach (var personnel in personnelList)
            {
                // Send News logic goes here if needed

                // Record in SendNewsLog
                _context.SendNewsletterLogs.Add(new SendNewsletterLog
                {
                    PersonnelId = personnel.Id,
                    SendTime = DateTime.UtcNow,
                    SendStatus = SendStatus.Sent,
                    NewsletterId = News.Id,
                    NewsTitle = News.Title,
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


