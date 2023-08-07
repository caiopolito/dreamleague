using dreamleague.domain.Aggregates.SendMessage;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Chat
{
    public interface ISendMessageService : IService<SendMessageRequest, SendMessageResponse>
    {
    }
}
