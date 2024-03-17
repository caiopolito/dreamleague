using dreamleague.common.Entities.Steam;
using dreamleague.shared.Infrastructures;

namespace dreamleague.domain.Infrastructure
{
    public interface ISteamUserRepository
    {
        Task<GenericHttpResponse<SteamResponses>> GetPlayerSummariesAsync(string[] steamids);
        Task<GenericHttpResponse<SteamResponses>> GetPlayerFriendsAsync(string steamid);
    }
}
