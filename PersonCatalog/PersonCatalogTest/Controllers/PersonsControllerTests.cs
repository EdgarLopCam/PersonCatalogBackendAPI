namespace PersonCatalogTest.Controllers
{
    using Moq;
    using Microsoft.AspNetCore.Mvc;
    using PersonCatalog.Controllers;
    using Domain.Entities;
    using Application.Persons.Commands;
    using Application.Persons.Queries;
    using MediatR;

    public class PersonsControllerTests
    {
        private readonly PersonsController _controller;
        private readonly Mock<IMediator> _mockMediator;

        public PersonsControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new PersonsController(_mockMediator.Object);
        }

        [Fact]
        public async Task GetAllPersons_ReturnsOkResult_WithListOfPersons()
        {
            // Arrange: Configura el mock de MediatR para devolver una lista de personas
            var persons = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe" },
            new Person { Id = 2, FirstName = "Jane", LastName = "Doe" }
        };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllPersonsQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(persons);

            // Act: Llama al método GetAllPersons del controlador
            var result = await _controller.GetAllPersons();

            // Assert: Verifica que el resultado sea un OkObjectResult con la lista de personas
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnPersons = Assert.IsType<List<Person>>(okResult.Value);
            Assert.Equal(2, returnPersons.Count);  // Verifica que la lista devuelta tenga dos personas
        }

        [Fact]
        public async Task GetPersonById_ReturnsNotFound_WhenPersonDoesNotExist()
        {
            // Arrange: Configura el mock de MediatR para devolver null cuando se llame a GetPersonByIdQuery
            _mockMediator.Setup(m => m.Send(It.IsAny<GetPersonByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync((Person)null);

            // Act: Llama al método GetPersonById del controlador
            var result = await _controller.GetPersonById(1);

            // Assert: Verifica que el resultado sea NotFound
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreatePerson_ReturnsOk_WhenPersonIsCreated()
        {
            // Arrange: Configura el mock de MediatR para devolver el ID de la persona creada
            var createCommand = new CreatePersonCommand("New", "Person", "new.person@example.com", new DateTime(1990, 1, 1), "1234567890");
            var expectedPersonId = 3;  // Simula que el ID de la nueva persona es 3

            _mockMediator.Setup(m => m.Send(It.IsAny<CreatePersonCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedPersonId);  // Devuelve el ID simulado

            // Act: Llama al método CreatePerson del controlador con el command
            var result = await _controller.CreatePerson(createCommand);

            // Assert: Verifica que el resultado sea OkObjectResult con el ID de la persona creada
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedPersonId, okResult.Value);  // Verifica que el ID devuelto sea el esperado
        }

        [Fact]
        public async Task UpdatePerson_ReturnsOkResult_WhenPersonIsUpdated()
        {
            // Arrange: Configura el mock de MediatR para devolver una entidad Person actualizada
            var updateCommand = new UpdatePersonCommand(1, "John", "Doe", "john.doe@example.com", new DateTime(1990, 1, 1), "1234567890", new byte[] { });
            var updatedPerson = new Person { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

            // Simula que el Mediator devuelve una persona actualizada
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdatePersonCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(updatedPerson);

            // Act: Llama al método UpdatePerson del controlador con el command
            var result = await _controller.UpdatePerson(updateCommand.Id, updateCommand);

            // Assert: Verifica que el resultado sea OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Person>(okResult.Value);
            Assert.Equal(updateCommand.Id, returnValue.Id);  // Verifica que el ID sea el mismo
        }

        [Fact]
        public async Task DeletePerson_ReturnsNoContent_WhenPersonIsDeleted()
        {
            // Arrange: Configura el mock de MediatR para que el método de eliminación devuelva verdadero
            _mockMediator.Setup(m => m.Send(It.IsAny<DeletePersonCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            // Act: Llama al método DeletePerson del controlador
            var result = await _controller.DeletePerson(1);

            // Assert: Verifica que el resultado sea NoContent
            Assert.IsType<NoContentResult>(result);
        }
    }
}
