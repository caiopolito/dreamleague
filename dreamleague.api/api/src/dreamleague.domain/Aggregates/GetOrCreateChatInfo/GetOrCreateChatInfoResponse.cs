using dreamleague.common.Entities.Chat;

namespace dreamleague.domain.Aggregates.GetOrCreateChatInfo
{
    public class GetOrCreateChatInfoResponse
    {
        public Guid ChatId { get; set; }
        public IEnumerable<Message> Messages { get; set; }

        public static implicit operator GetOrCreateChatInfoResponse(ChatInfo entity)
        {
            return new GetOrCreateChatInfoResponse
            {
                ChatId = entity.ChatId,
                Messages = entity.Messages
            };
        }
    }
}
