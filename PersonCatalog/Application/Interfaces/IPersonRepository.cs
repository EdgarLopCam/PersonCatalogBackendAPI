namespace Application.Interfaces
{
    using Domain.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPersonRepository
    {
        Task<Person> GetPersonByIdAsync(int id);
        Task<List<Person>> GetAllPersonsAsync();
        Task<int> AddPersonAsync(Person person);
        Task<Person> UpdatePersonAsync(Person person);
        Task<bool> DeletePersonAsync(int id);
    }
}
