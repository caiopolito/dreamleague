namespace dreamleague.domain.Entities.Chat
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public DateTime MessageTime { get; set; }
        public string MessageTimeFormatted { get => MessageTime.ToString("HH:mm:ss"); }
        public string Content { get; set; }
    }
}
