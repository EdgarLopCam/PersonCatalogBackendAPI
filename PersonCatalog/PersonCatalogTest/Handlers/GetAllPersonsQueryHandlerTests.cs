namespace PersonCatalogTest.Handlers
{
    using Application.Interfaces;
    using Application.Persons.Handlers;
    using Application.Persons.Queries;
    using Domain.Entities;
    using Moq;

    public class GetAllPersonsQueryHandlerTests
    {
        private readonly Mock<IPersonRepository> _mockRepository;
        private readonly GetAllPersonsQueryHandler _handler;

        public GetAllPersonsQueryHandlerTests()
        {
            _mockRepository = new Mock<IPersonRepository>();
            _handler = new GetAllPersonsQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfPersons()
        {
            // Arrange
            var persons = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe" },
            new Person { Id = 2, FirstName = "Jane", LastName = "Doe" }
        };
            _mockRepository.Setup(repo => repo.GetAllPersonsAsync()).ReturnsAsync(persons);

            // Act
            var result = await _handler.Handle(new GetAllPersonsQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("John", result[0].FirstName);
        }

    }
}
