using dreamleague.common.Entities.Notifications;

namespace dreamleague.domain.Aggregates.GetUserNotifications
{
    public class GetUserNotificationsResponse
    {
        public IEnumerable<TeamNotification> TeamNotifications { get; set; }
        public int NotSeen { get => TeamNotifications.Count(x => !x.IsSeen); }
    }
}
