namespace Application.Persons.Commands
{
    using MediatR;

    public record CreatePersonCommand(string FirstName, string LastName, string Email, DateTime DateOfBirth, string PhoneNumber) : IRequest<int>;
}
