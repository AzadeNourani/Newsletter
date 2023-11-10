

using NewsletterAPI.Data.Contexts;

namespace NewsletterAPI.Data.Services.GetPersonnelList
{
    public interface IGetPersonnelListService
    {
        Task<List<PersonnelListDto>> ExecuteAsync();
    }
   
    public class PersonnelListDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
     
    }
}
