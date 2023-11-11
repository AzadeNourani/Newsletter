using NewsletterAPI.Data.Models;

namespace NewsletterAPI.Data.Services.Queries.GetLastNews
{
    public interface IGetLastNewsService
    {
        Task<Newsletter> ExecuteAsync();        
    }
}
