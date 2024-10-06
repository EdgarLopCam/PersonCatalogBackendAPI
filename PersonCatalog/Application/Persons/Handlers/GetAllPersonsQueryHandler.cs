namespace Application.Persons.Handlers
{
    using Application.Interfaces;
    using Application.Persons.Queries;
    using Domain.Entities;
    using MediatR;

    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, List<Person>>
    {
        private readonly IPersonRepository _personRepository;

        public GetAllPersonsQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<List<Person>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            return await _personRepository.GetAllPersonsAsync();
        }
    }
}
