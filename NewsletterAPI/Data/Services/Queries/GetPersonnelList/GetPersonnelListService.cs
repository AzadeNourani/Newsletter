using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NewsletterAPI.Data.Contexts;
using NewsletterAPI.DTOs;

namespace NewsletterAPI.Data.Services.Queries.GetPersonnelList
{
    public class GetPersonnelListService : IGetPersonnelListService
    {
        private readonly NewsDbContext _context;
        public GetPersonnelListService(NewsDbContext context)
        {
            _context = context;

        } 

        public async Task<List<PersonnelListDTO>> ExecuteAsync()
        {
            const int batchSize = 100;
            var personnelList = new List<PersonnelListDTO>();
            int totalRecordsProcessed = 0;

            while (true)
            {
                var batch = await _context.Personnels
                    .Skip(totalRecordsProcessed)
                    .Take(batchSize)
                    .Select(p => new PersonnelListDTO
                    {
                        Id  = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        NationalCode = p.NationalCode
                    })
                    .ToListAsync();

                if (batch.Count == 0)
                {
                    break; // No more records
                }

                personnelList.AddRange(batch);
                totalRecordsProcessed += batch.Count;

                if (totalRecordsProcessed >= 1000) // To Do
                {
                    break; // All Personnels have recieved News
                }
            }

            return personnelList;
        }

    }
}
