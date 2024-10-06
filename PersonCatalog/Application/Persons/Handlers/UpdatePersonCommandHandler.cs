namespace Application.Persons.Handlers
{
    using Application.Interfaces;
    using Application.Persons.Commands;
    using Domain.Entities;
    using MediatR;

    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Person>
    {
        private readonly IPersonRepository _personRepository;

        public UpdatePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                RowVersion = request.RowVersion
            };

            var updatedPerson = await _personRepository.UpdatePersonAsync(person);

            if (updatedPerson == null)
            {
                throw new Exception("Error de concurrencia: el registro fue modificado por otro usuario.");
            }

            return updatedPerson;
        }
    }
}
