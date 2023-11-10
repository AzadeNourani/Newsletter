using Microsoft.EntityFrameworkCore;
using NewsletterAPI.Data.Models;
using System;

public interface ISendNewsToPersonnelListService
{
   
    public interface ISendNewsToPersonnelListService
    {
        Task SendNewsToPersonnelList(List<PersonnelDto> personnelList);
    }

    public class PersonnelDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }


    }

    //object DeliveryReports { get; set; }

    ////DbSet<NewsletterSentLog> NewsletterSentLogs { get; set; }

    //List<Personnel> GetAllPersonnels();

    //Personnel GetPersonById(int personId);

    //Newsletter GetNewsletterByDate(DateTime date);
    //void SaveNewsletter(Newsletter newsletter);

    //void LogSentNewsletter(SendNewsletterLog report);

    //int SaveChanges();
}
