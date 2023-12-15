using NewsletterAPI.Data.Contexts;
using NewsletterAPI.DTOs;

namespace NewsletterAPI.Data.Services.Queries.GetPersonnelList
{
    public interface IGetPersonnelListService
    {
        Task<List<PersonnelListDTO>> ExecuteAsync();
    }
}
