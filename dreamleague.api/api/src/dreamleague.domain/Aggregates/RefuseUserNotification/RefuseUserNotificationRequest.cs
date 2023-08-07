namespace dreamleague.domain.Aggregates.RefuseUserNotification
{
    public class RefuseUserNotificationRequest
    {
        public string SteamId { get; set; }
        public Guid NotificationId { get; set; }
    }
}
