using dreamleague.common.Entities.Chat;

namespace dreamleague.domain.Aggregates.SendMessage
{
    public class SendMessageRequest
    {
        public ChatInfo Chat { get; set; }
        public string? Message { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public DateTime MessageTime { get => DateTime.Now; }
    }
}
