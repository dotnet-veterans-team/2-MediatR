using MediatR;
using MediatRPoC.Application.Commands;
using MediatRPoC.Application.Models;
using MediatRPoC.Application.Notifications;
using MediatRPoC.Interfaces;

namespace MediatRPoC.Application.Handlers
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Person> _repository;
        
        public UpdatePersonCommandHandler(IMediator mediator, IRepository<Person> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person { 
                Id = request.Id,
                Name = request.Name, 
                Age = request.Age, 
                Gender = request.Gender 
            };

            try
            {
                person = await _repository.Update(person);

                await _mediator.Publish(new PersonUpdatedNotification { Id = person.Id, Name = person.Name, Age = person.Age, Gender = person.Gender, IsComplete = true });

                return await Task.FromResult("Person updated with success");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new PersonUpdatedNotification { Id = person.Id, Name = person.Name, Age = person.Age, Gender = person.Gender, IsComplete = false });
                await _mediator.Publish(new ErrorNotification { ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
                return await Task.FromResult("An error ocurred on update");
            }
        }
    }
}
