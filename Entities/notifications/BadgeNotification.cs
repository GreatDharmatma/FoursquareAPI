
namespace Brahmastra.FoursquareAPI.Entities.Notifications
{
    public class BadgeNotification
    {
        public Badge Badge { get; private set; }

        public BadgeNotification(Badge badge)
        {
            Badge = badge;
        }
    }
}
