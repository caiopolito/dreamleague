namespace dreamleague.common.Entities.Notifications
{
    public class TeamNotification
    {
        public Guid Id { get; set; }
        public string SteamId { get; set; }
        public Guid TeamId { get; set; }
        public string TeamName { get; set; }
        public bool IsResponded { get; set; }
        public DateTime RespondedAt { get; set; }
        public bool IsSeen { get; set; }
        public DateTime SeenAt { get; set; }
    }
}
