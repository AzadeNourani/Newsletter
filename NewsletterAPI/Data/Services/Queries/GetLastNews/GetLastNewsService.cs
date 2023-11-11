using Microsoft.EntityFrameworkCore;
using NewsletterAPI.Data.Contexts;
using NewsletterAPI.Data.Models;

namespace NewsletterAPI.Data.Services.Queries.GetLastNews
{
    public class GetLastNewsService : IGetLastNewsService
    {
        private readonly NewsDbContext _dbContext;
      
        public GetLastNewsService(NewsDbContext dbContext )
        {
            _dbContext= dbContext ?? throw new ArgumentNullException(nameof(dbContext)); ;
        }

        public async Task<Newsletter> ExecuteAsync()
        {

            var lastNewsletter=await _dbContext.Newsletter
            .OrderByDescending(newsletter => newsletter.SendDate)
            .FirstOrDefaultAsync();
            if (lastNewsletter != null)
            {               
                return lastNewsletter;
            }
            else
            {
                var defaultNewsletter = new Newsletter
                {
                    Id = 1,
                    // To do
                };

                return defaultNewsletter;
            }
        }
    }
}
