namespace Application.Persons.Queries
{
    using Domain.Entities;
    using MediatR;

    public record GetPersonByIdQuery(int Id) : IRequest<Person>;
}
