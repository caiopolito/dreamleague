namespace dreamleague.domain.Aggregates.AcceptUserNotification
{
    public class AcceptUserNotificationRequest
    {
        public string SteamId { get; set; }
        public Guid NotificationId { get; set; }
    }
}
