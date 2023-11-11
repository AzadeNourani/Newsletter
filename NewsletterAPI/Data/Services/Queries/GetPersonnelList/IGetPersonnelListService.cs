using NewsletterAPI.Data.Contexts;
using NewsletterAPI.Dto;

namespace NewsletterAPI.Data.Services.Queries.GetPersonnelList
{
    public interface IGetPersonnelListService
    {
        Task<List<PersonnelListDto>> ExecuteAsync();
    }
}
