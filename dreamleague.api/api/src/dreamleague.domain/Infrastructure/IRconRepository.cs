using CoreRCON.Parsers.Standard;
using dreamleague.common.Entities.Rcon;
using dreamleague.common.Entities.Servers;

namespace dreamleague.domain.Infrastructure
{
    public interface IRconRepository
    {
        Task<Status> CheckConnectionAsync(Server server);
        Task<RconAvailable> CheckAvailabilityAsync(Server server);
        Task StartMatchInServerAsync(Server server, string matchId);
        Task SetGet5ApiKeyAsync(Server server, string apiKey);
    }
}
