namespace Application.Persons.Handlers
{
    using Application.Interfaces;
    using Application.Persons.Commands;
    using MediatR;

    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IPersonRepository _personRepository;

        public DeletePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            return await _personRepository.DeletePersonAsync(request.Id);
        }
    }
}
