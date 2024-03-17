using dreamleague.common.Entities.Chat;

namespace dreamleague.domain.Aggregates.SendMessage
{
    public class SendMessageResponse
    {
        public Guid? ChatId { get; set; }
        public IEnumerable<Message> Messages { get; set; }

        public static implicit operator SendMessageResponse(ChatInfo entity)
        {
            return new SendMessageResponse
            {
                ChatId = entity?.ChatId,
                Messages = entity?.Messages
            };
        }
    }
}
