using MediatR;
using MediatRPoC.Application.Commands;
using MediatRPoC.Application.Models;
using MediatRPoC.Application.Notifications;
using MediatRPoC.Interfaces;

namespace MediatRPoC.Application.Handlers
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Person> _repository;
        
        public DeletePersonCommandHandler(IMediator mediator, IRepository<Person> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(request.Id);

                await _mediator.Publish(new PersonDeletedNotification { Id = request.Id, IsComplete = true });

                return await Task.FromResult("Person deleted with success");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new PersonDeletedNotification { Id = request.Id, IsComplete = false });
                await _mediator.Publish(new ErrorNotification { ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
                return await Task.FromResult("An error ocurred on exclusion");
            }
        }
    }
}
