namespace PersonCatalogTest.Commands
{
    using Application.Interfaces;
    using Application.Persons.Commands;
    using Application.Persons.Handlers;
    using Domain.Entities;
    using Moq;

    public class CreatePersonCommandHandlerTests
    {
        private readonly Mock<IPersonRepository> _mockRepository;
        private readonly CreatePersonCommandHandler _handler;

        public CreatePersonCommandHandlerTests()
        {
            _mockRepository = new Mock<IPersonRepository>();
            _handler = new CreatePersonCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallAddPerson_WhenPersonIsCreated()
        {
            // Arrange
            var command = new CreatePersonCommand("John", "Doe", "john.doe@example.com", new DateTime(1990, 1, 1), "1234567890");

            _mockRepository.Setup(repo => repo.AddPersonAsync(It.IsAny<Person>())).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockRepository.Verify(repo => repo.AddPersonAsync(It.IsAny<Person>()), Times.Once);
            Assert.Equal(1, result);
        }
    }
}
