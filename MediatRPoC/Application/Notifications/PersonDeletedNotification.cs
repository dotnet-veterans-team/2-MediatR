using MediatR;

namespace MediatRPoC.Application.Notifications
{
    public class PersonDeletedNotification : INotification
    {
        public int Id { get; set; }
        public bool IsComplete { get; set; }
    }
}
