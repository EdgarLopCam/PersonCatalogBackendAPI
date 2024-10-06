//using Application.Interfaces;
//using Application.Persons.Commands;
//using Application.Persons.Handlers;
//using Domain.Entities;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Metadata;
//using System.Text;
//using System.Threading.Tasks;

//namespace PersonCatalogTest.Handlers
//{
//    public class UpdatePersonCommandHandlerTests
//    {
//        private readonly Mock<IPersonRepository> _mockRepository;
//        private readonly GetAllPersonsQueryHandler _handler;

//        public UpdatePersonCommandHandlerTests()
//        {
//            _mockRepository = new Mock<IPersonRepository>();
//            _handler = new GetAllPersonsQueryHandler(_mockRepository.Object);
//        }
//        [Fact]
//        public async Task Handle_ShouldCallUpdatePerson_WhenPersonIsUpdated()
//        {
//            // Arrange
//            var command = new UpdatePersonCommand(1, "John", "Doe", "john.doe@example.com", new DateTime(1990, 1, 1), "1234567890", new byte[] { });

//            // Persona existente que será actualizada
//            var personToUpdate = new Person
//            {
//                Id = 1,
//                FirstName = "John",
//                LastName = "Doe",
//                Email = "old.email@example.com",
//                DateOfBirth = new DateTime(1990, 1, 1),
//                PhoneNumber = "1234567890"
//            };

//            // Configura el mock para que GetPersonByIdAsync devuelva la persona existente
//            _mockRepository.Setup(repo => repo.GetPersonByIdAsync(command.Id))
//                           .ReturnsAsync(personToUpdate);

//            // Configura el mock para que UpdatePersonAsync devuelva la persona actualizada
//            _mockRepository.Setup(repo => repo.UpdatePersonAsync(It.IsAny<Person>()))
//                           .ReturnsAsync(personToUpdate);  // Simula que la persona fue actualizada

//            // Act
//            var result = await _handler.Handle(command, CancellationToken.None);

//            // Assert: Verifica que el método UpdatePersonAsync fue llamado exactamente una vez
//            _mockRepository.Verify(repo => repo.UpdatePersonAsync(It.IsAny<Person>()), Times.Once);

//            // Verifica que los valores de la persona devuelta sean los valores actualizados
//            Assert.Equal("John", result.FirstName);
//            Assert.Equal("Doe", result.LastName);
//            Assert.Equal("john.doe@example.com", result.Email);
//            Assert.Equal("1234567890", result.PhoneNumber);
//            Assert.Equal(new DateTime(1990, 1, 1), result.DateOfBirth);
//        }
//    }
//}
