using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NewsletterAPI.Data.Contexts;
using NewsletterAPI.Dto;

namespace NewsletterAPI.Data.Services.Queries.GetPersonnelList
{
    public class GetPersonnelListService : IGetPersonnelListService
    {
        private readonly NewsDbContext _context;
        public GetPersonnelListService(NewsDbContext context)
        {
            _context = context;

        }

        //public async Task<List<PersonnelListDto>> ExecuteAsync()
        //{
        //    const int batchSize = 100;

        //    var personnelList = new List<PersonnelListDto>();

        //    for (int skip = 0; ; skip += batchSize)
        //    {
        //        var batch = await _context.Personnels
        //            .Skip(skip)
        //            .Take(batchSize)
        //            .Select(p => new PersonnelListDto
        //            {
        //                Id = p.Id,
        //                FirstName = p.FirstName,
        //                LastName = p.LastName,
        //                NationalCode = p.NationalCode
        //            })
        //            .ToListAsync();

        //        if (batch.Count == 0)
        //        {
        //            break; // No more records
        //        }

        //        personnelList.AddRange(batch);
        //    }

        //    return personnelList;
        //}

        public async Task<List<PersonnelListDto>> ExecuteAsync()
        {
            const int batchSize = 100;
            var personnelList = new List<PersonnelListDto>();
            int totalRecordsProcessed = 0;

            while (true)
            {
                var batch = await _context.Personnels
                    .Skip(totalRecordsProcessed)
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
