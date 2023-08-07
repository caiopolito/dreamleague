using dreamleague.domain.Entities.Players;

namespace dreamleague.domain.Infrastructure
{
    public interface IPlayerRepository : IDatabaseRepository
    {
        Task<Player> GetPlayerInfoBySteamIdAsync(string steamId);
        Task<Player> InsertPlayerAsync(string steamId, string name, Uri avatar);
        Task<Player> UpdatePlayerNameAsync(string steamId, string name, Uri avatar);
        Task UpdatePlayerAsync(Player player);
        Task<IEnumerable<Player>> GetAllPlayersAsync(PlayerFilter filter = null);
        Task<bool> CheckIfHasTeamAsync(string steamId, Guid championshipId);
    }
}
