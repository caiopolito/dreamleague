using dreamleague.common.Entities.Rcon;

namespace dreamleague.domain.Infrastructure
{
    public interface IMatchStorageRepository
    {
        Task CreateMatchJsonFileAsync(RconMatch match);
    }
}
