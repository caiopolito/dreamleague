using dreamleague.domain.Entities.Servers;

namespace dreamleague.domain.Infrastructure
{
    public interface IServerRepository : IDatabaseRepository
    {
        Task<Server> GetFirstAvailableServerAsync();
        Task<Server> GetServerByIdAsync(Guid serverId);
        Task UpdateServerStatusAsync(Guid serverId, bool status);
    }
}
