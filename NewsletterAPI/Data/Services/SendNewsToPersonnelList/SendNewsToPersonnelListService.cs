using Hangfire;
using Microsoft.EntityFrameworkCore;
using NewsletterAPI.Data.Models;
using static ISendNewsToPersonnelListService;

public class SendNewsToPersonnelListService:ISendNewsToPersonnelListService
{
    private readonly ISendNewsToPersonnelListService _sendNewsToPersonnelListService;
      
    //private readonly IBackgroundJobClient _backgroundJobClient;

    //public NewsletterService(IBackgroundJobClient backgroundJobClient)
    //{
    //    _backgroundJobClient = backgroundJobClient;
    //}
    public SendNewsToPersonnelListService(ISendNewsToPersonnelListService sendNewsToPersonnelListService)
    {
        _sendNewsToPersonnelListService = sendNewsToPersonnelListService;
    }

    public async Task SendNewsToPersonnelList(List<PersonnelDto> personnelList)
    {
        foreach (var personnel in personnelList)
        {
            // Assuming you have a method in SendNewsService to send news
            await _sendNewsToPersonnelListService.SendNewsToPersonnel(personnel.Id, "Hardcoded News Content");
        }
    }

    //public void LogSentNewslettersForAllPersonnel()
    //{
    //    var allPersonnel = GetAllPersonnels();

    //    foreach (var personnel in allPersonnel)
    //    {
    //        // Schedule the job to run daily
    //        //LogSentNewsletter(personnel.Id,1);
    //        RecurringJob.AddOrUpdate(() => LogSentNewsletter(personnel.Id, 2), Cron.Daily);
    //    }
    //}
    //public void LogSentNewsletter(int personnelId,int newsletterId)
    //{
    //    var report = new SendNewsletterLog
    //    {
    //        PersonnelId = personnelId,
    //        NewsletterId= newsletterId,
    //        SendStatus = SendStatus.Sent,
    //        SendTime = DateTime.UtcNow,
    //        ReceiveTime = DateTime.UtcNow, // Set to the actual receive time
    //        // ViewTime = DateTime.UtcNow // Set to the actual view time
    //    };

    //    // Log the sent newsletter in the database
    //    //   _newsletterDbContext.LogSentNewsletter(report);
    //    BackgroundJob.Enqueue(() => _newsletterService.LogSentNewsletter(report));

    //    _newsletterService.SaveChanges();
    //}

}