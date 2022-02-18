using MediatR;
using MediatRPoC.Application.Commands;
using MediatRPoC.Application.Models;
using MediatRPoC.Application.Queries;
using MediatRPoC.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediatRPoC.Application.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonController : Controller
    {
        private readonly IMediator _mediator; 
        private readonly IRepository<Person> _repository;

        public PersonController(IMediator mediator, IRepository<Person> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _mediator.Send(new GetPersonQuery() { Id = id });
            if (response != null && response.Id > 0)
                return Ok(response);
            else
                return NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePersonCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdatePersonCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = new DeletePersonCommand { Id = id };
            var result = await _mediator.Send(obj);
            return Ok(result);
        }
    }
}
