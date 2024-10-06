namespace Application.Persons.Commands
{
    using MediatR;

    public record DeletePersonCommand(int Id) : IRequest<bool>;
}
