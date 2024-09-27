using TechShop.Domain.Notifications;

namespace TechShop.Infrasctructure.Notifications
{
    public class Notificator : INotificator
    {
        private List<Notification> _notifications;
        public Notificator() => _notifications = new List<Notification>();
        public List<Notification> GetNotifications() => _notifications;
        public bool HasNotification() => _notifications.Any();
        public void Notify(Notification notification) => _notifications.Add(notification);  
    }
}
