using dreamleague.domain.Aggregates.GetOrCreateChatInfo;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Chat
{
    public interface IGetOrCreateChatInfoService : IService<GetOrCreateChatInfoRequest, GetOrCreateChatInfoResponse>
    {
    }
}
