using MediatR;
using MediatRPoC.Application.Models;
using MediatRPoC.Application.Notifications;
using MediatRPoC.Application.Queries;
using MediatRPoC.Interfaces;

namespace MediatRPoC.Application.Handlers
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, Person>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Person> _repository;

        public GetPersonQueryHandler(IMediator meditor, IRepository<Person> repository)
        {
            _mediator = meditor;
            _repository = repository;
        }

        public async Task<Person> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var person = await _repository.Get(request.Id);

                await _mediator.Publish(new PersonFetchedNotification { Id = person.Id, Name = person.Name, Age = person.Age, Gender = person.Gender, IsComplete = true });

                return await Task.FromResult(person);
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new PersonFetchedNotification { Id = request.Id, IsComplete = false });
                await _mediator.Publish(new ErrorNotification { ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
                return await Task.FromResult(new Person() { Id = request.Id });
            }
        }
    }
}
