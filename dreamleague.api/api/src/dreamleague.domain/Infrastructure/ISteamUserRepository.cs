using dreamleague.domain.Entities.Steam;

namespace dreamleague.domain.Infrastructure
{
    public interface ISteamUserRepository
    {
        Task<SteamResponses> GetPlayerSummariesAsync(string[] steamids);
        Task<SteamResponses> GetPlayerFriendsAsync(string steamid);
    }
}
