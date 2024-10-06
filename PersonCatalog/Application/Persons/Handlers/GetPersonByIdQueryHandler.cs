namespace Application.Persons.Handlers
{
    using Application.Interfaces;
    using Application.Persons.Queries;
    using Domain.Entities;
    using MediatR;

    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, Person>
    {
        private readonly IPersonRepository _personRepository;

        public GetPersonByIdQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            return await _personRepository.GetPersonByIdAsync(request.Id);
        }
    }
}
