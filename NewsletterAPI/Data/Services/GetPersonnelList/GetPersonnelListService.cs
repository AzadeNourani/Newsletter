using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NewsletterAPI.Data.Contexts;

namespace NewsletterAPI.Data.Services.GetPersonnelList
{
    public class GetPersonnelListService : IGetPersonnelListService
    {
        private readonly NewsDbContext _context;
        public GetPersonnelListService(NewsDbContext context)
        {
            _context = context;

        }    

        public async Task<List<PersonnelListDto>> ExecuteAsync()
        {
            const int batchSize = 1000; 

            var personnelList = new List<PersonnelListDto>();

            for (int skip = 0; ; skip += batchSize)
            {
                var batch = await _context.Personnels
                    .Skip(skip)
                    .Take(batchSize)
                    .Select(p => new PersonnelListDto
                    {
                        Id = p.Id,
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
            }

            return personnelList;
        }
    }
}
