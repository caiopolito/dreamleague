using dreamleague.domain.Aggregates.GetOrCreateChatInfo;

namespace dreamleague.domain.Entities.Chat
{
    public class ChatInfo
    {
        public ChatInfo()
        {
            Messages = new List<Message>();
        }
        public Guid ChatId { get; set; }
        public IEnumerable<Message> Messages { get; set; }

        public static implicit operator ChatInfo(GetOrCreateChatInfoResponse entity)
        {
            return new ChatInfo
            {
                ChatId = entity.ChatId,
                Messages = entity.Messages
            };
        }
    }
}
