namespace Infrastructure.Repositories
{
    using Application.Interfaces;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddPersonAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person.Id;
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _context.Persons.FindAsync(id);
        }

        public async Task<List<Person>> GetAllPersonsAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person> UpdatePersonAsync(Person person)
        {
            _context.Persons.Update(person);
            try
            {
                await _context.SaveChangesAsync();
                return person;  // Devuelve la persona actualizada
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;  // Devuelve null si falla la concurrencia
            }
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null) return false;

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
