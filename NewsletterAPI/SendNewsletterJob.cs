using NewsletterAPI.Data.Services.Queries.GetLastNews;
using NewsletterAPI.Data.Services.Queries.GetPersonnelList;

namespace NewsletterAPI
{
    public class SendNewsletterJob
    {
        private readonly GetLastNewsService _getLastNewsService;
        private readonly GetPersonnelListService _getPersonnelListService;

        public SendNewsletterJob(GetLastNewsService getLastNewsService, GetPersonnelListService getPersonnelListService)
        {
            _getLastNewsService = getLastNewsService;
            _getPersonnelListService = getPersonnelListService;
        }

        public void SendNewsletters()
        {
            var lastNews = _getLastNewsService.ExecuteAsync().Result; // Synchronous call for simplicity
            var personnelList = _getPersonnelListService.ExecuteAsync().Result; // Synchronous call for simplicity

            // Send newsletters to personnelList using lastNews
            foreach (var personnel in personnelList)
            {
                // Send newsletter to personnel
                // You may need to implement the logic to send newsletters here
                // e.g., using a dedicated service or sending emails
                Console.WriteLine($"Sending newsletter to personnel: {personnel.Id}, News: {lastNews.Title}");
            }
        }
    }
}
