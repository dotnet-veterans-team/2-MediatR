using MediatR;
using MediatRPoC.Application.Models;

namespace MediatRPoC.Application.Queries
{
    public class GetPersonQuery : IRequest<Person>
    {
        public int Id { get; set; }
    }
}
