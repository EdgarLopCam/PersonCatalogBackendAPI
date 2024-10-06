//namespace PersonCatalogTest.Repositories
//{
//    using Domain.Entities;
//    using Infrastructure.Repositories;
//    using Infrastructure;
//    using Microsoft.EntityFrameworkCore;

//    public class PersonRepositoryTests
//    {
//        private readonly PersonRepository _repository;
//        private readonly ApplicationDbContext _context;

//        public PersonRepositoryTests()
//        {
//            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//                .UseInMemoryDatabase(databaseName: "TestDatabase")
//                .Options;
//            _context = new ApplicationDbContext(options);
//            _repository = new PersonRepository(_context);
//        }

//        [Fact]
//        public async Task AddPerson_AddsPersonToDatabase()
//        {
//            // Arrange: Crea una persona con todos los campos obligatorios
//            var person = new Person
//            {
//                FirstName = "John",
//                LastName = "Doe",
//                Email = "john.doe@example.com",
//                DateOfBirth = new DateTime(1990, 1, 1),
//                PhoneNumber = "1234567890"
//            };

//            // Act: Añade la persona a la base de datos en memoria
//            await _repository.AddPersonAsync(person);
//            await _context.SaveChangesAsync();

//            // Assert: Verifica que la persona se haya añadido a la base de datos
//            var personsInDb = _context.Persons.ToList();
//            Assert.Single(personsInDb);
//            Assert.Equal("John", personsInDb[0].FirstName);
//        }
//    }
//}
