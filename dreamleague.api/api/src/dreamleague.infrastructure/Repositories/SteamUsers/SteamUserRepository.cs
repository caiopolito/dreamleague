using dreamleague.common.Entities.Steam;
using dreamleague.domain.Infrastructure;
using dreamleague.shared.Configurations;
using dreamleague.shared.Infrastructures;

namespace dreamleague.infrastructure.Repositories.SteamUsers
{
    public class SteamUserRepository : GenericHttpRepository, ISteamUserRepository
    {
        private readonly ApplicationConfig _applicationConfig;
        private const string PlayerSummariesEndpoint = "GetPlayerSummaries/v0002/";
        private const string PlayerFriendsEndpoint = "GetFriendList/v0001/";
        public SteamUserRepository(HttpClient httpClient, ApplicationConfig applicationConfig) : base(httpClient)
        {
            _applicationConfig = applicationConfig;
        }

        public async Task<GenericHttpResponse<SteamResponses>> GetPlayerSummariesAsync(string[] steamids)
        {
            var parameters = new Dictionary<string, string>() { { "key", _applicationConfig.Authentication.SteamApiKey } };
            var steamidsquery = string.Join(",", steamids);
            parameters.TryAdd("steamids", steamidsquery);
            return await GetAsync<SteamResponses>($"{PlayerSummariesEndpoint}", parameters);
        }

        public async Task<GenericHttpResponse<SteamResponses>>
            GetPlayerFriendsAsync(string steamid)
        {
            var parameters = new Dictionary<string, string>() { { "key", _applicationConfig.Authentication.SteamApiKey } };
            parameters.TryAdd("steamid",steamid);
            parameters.TryAdd("relationship", "friend");
            
            return await GetAsync<SteamResponses>($"{PlayerFriendsEndpoint}", parameters);
        }
    }
}
