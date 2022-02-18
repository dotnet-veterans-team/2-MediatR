using MediatR;
using MediatRPoC.Application.Commands;
using MediatRPoC.Application.Models;
using MediatRPoC.Application.Notifications;
using MediatRPoC.Interfaces;

namespace MediatRPoC.Application.Handlers
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Person> _repository;
        
        public CreatePersonCommandHandler(IMediator mediator, IRepository<Person> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person { 
                Name = request.Name, 
                Age = request.Age, 
                Gender = request.Gender 
            };

            try
            {
                person = await _repository.Add(person);

                await _mediator.Publish(new PersonCreatedNotification { Id = person.Id, Name = person.Name, Age = person.Age, Gender = person.Gender });

                return await Task.FromResult("Person created with success");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new PersonCreatedNotification { Id = person.Id, Name = person.Name, Age = person.Age, Gender = person.Gender });
                await _mediator.Publish(new ErrorNotification { ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
                return await Task.FromResult("An error ocurred on creation moment");
            }
        }
    }
}
