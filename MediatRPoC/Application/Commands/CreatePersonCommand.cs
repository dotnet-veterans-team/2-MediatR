using MediatR;

namespace MediatRPoC.Application.Commands
{
    public class  CreatePersonCommand : IRequest<string>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }
    }
}
