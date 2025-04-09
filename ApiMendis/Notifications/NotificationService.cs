namespace ApiMendis.Notifications
{
    public class NotificationService : INotificationService
    {
        public readonly IList<string> _notifications;

        public NotificationService()
        {
            _notifications = new List<string>();
        }

        public void Add(string value)
        {
            _notifications.Add(value);
        }
    }
}
