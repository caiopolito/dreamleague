using CoreRCON.Parsers.Standard;
using dreamleague.domain.Entities.Rcon;
using dreamleague.domain.Entities.Servers;

namespace dreamleague.domain.Infrastructure
{
    public interface IRconRepository
    {
        Task<Status> CheckConnectionAsync(Server server);
        Task<RconAvailable> CheckAvailabilityAsync(Server server);
        Task StartMatchInServerAsync(Server server, string fileName);
        Task SetGet5ApiKeyAsync(Server server, string apiKey);
    }
}
