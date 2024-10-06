namespace Application.Persons.Commands
{
    using Domain.Entities;
    using MediatR;

    public record UpdatePersonCommand(int Id, string FirstName, string LastName, string Email, DateTime DateOfBirth, string PhoneNumber, byte[] RowVersion) : IRequest<Person>;
}
