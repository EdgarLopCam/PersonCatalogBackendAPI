namespace PersonCatalog.Controllers
{
    using Application.Persons.Commands;
    using Application.Persons.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonCommand command)
        {
            var personId = await _mediator.Send(command);
            return Ok(personId);
        }

        // READ - Get All Persons
        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            var persons = await _mediator.Send(new GetAllPersonsQuery());
            return Ok(persons);
        }

        // READ - Get Person by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var person = await _mediator.Send(new GetPersonByIdQuery(id));
            if (person == null) return NotFound();
            return Ok(person);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] UpdatePersonCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID mismatch.");
            }

            var result = await _mediator.Send(command);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var result = await _mediator.Send(new DeletePersonCommand(id));
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
