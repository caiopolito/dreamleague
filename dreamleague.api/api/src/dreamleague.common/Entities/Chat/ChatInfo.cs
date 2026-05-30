namespace dreamleague.common.Entities.Chat
{
    public class ChatInfo
    {
        public ChatInfo()
        {
            Messages = new List<Message>();
        }
        public Guid ChatId { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
