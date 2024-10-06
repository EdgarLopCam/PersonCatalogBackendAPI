namespace Application.Persons.Queries
{
    using Domain.Entities;
    using MediatR;

    public record GetAllPersonsQuery : IRequest<List<Person>>;
}
