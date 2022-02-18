using MediatR;
using MediatRPoC.Application.Notifications;

namespace MediatRPoC.Application.EventHandlers
{
    public class LogEventHandler :
                            INotificationHandler<PersonCreatedNotification>,
                            INotificationHandler<PersonUpdatedNotification>,
                            INotificationHandler<PersonDeletedNotification>,
                            INotificationHandler<PersonFetchedNotification>,
                            INotificationHandler<ErrorNotification>
    {
        public Task Handle(PersonCreatedNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"CREATE: '{notification.Id} - {notification.Name} - {notification.Age} - {notification.Gender}'");
            });
        }

        public Task Handle(PersonUpdatedNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"UPDATE: '{notification.Id} - {notification.Name} - {notification.Age} - {notification.Gender} - {notification.IsComplete}'");
            });
        }

        public Task Handle(PersonDeletedNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"DELETE: '{notification.Id} - {notification.IsComplete}'");
            });
        }

        public Task Handle(PersonFetchedNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"GET: '{notification.Id} - {notification.Name} - {notification.Age} - {notification.Gender}'");
            });
        }

        public Task Handle(ErrorNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ERROR: '{notification.ExceptionMessage} \n {notification.StackTrace}'");
            });
        }
    }
}
