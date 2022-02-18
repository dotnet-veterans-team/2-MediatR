using MediatR;

namespace MediatRPoC.Application.Commands
{
    public class DeletePersonCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
