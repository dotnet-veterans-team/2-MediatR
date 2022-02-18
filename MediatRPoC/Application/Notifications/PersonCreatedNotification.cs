using MediatR;

namespace MediatRPoC.Application.Notifications
{
    public class PersonCreatedNotification : INotification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }
    }
}
