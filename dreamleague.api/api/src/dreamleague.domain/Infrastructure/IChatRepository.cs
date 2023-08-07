using dreamleague.domain.Aggregates.GetOrCreateChatInfo;
using dreamleague.domain.Aggregates.SendMessage;
using dreamleague.domain.Entities.Chat;

namespace dreamleague.domain.Infrastructure
{
    public interface IChatRepository : IDatabaseRepository
    {
        Task<ChatInfo> GetChatInfoAsync (GetOrCreateChatInfoRequest request);
        Task<ChatInfo> CreateChatAsync (GetOrCreateChatInfoRequest request);
        Task<ChatInfo> InsertAndRetrieveMessagesAsync(SendMessageRequest request);
    }
}
