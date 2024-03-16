using dreamleague.domain.Entities.Get5;

namespace dreamleague.domain.Infrastructure
{
    public interface IMatchRepository : IDatabaseRepository
    {
        Task<Match> CreateMatchAsync(Match match, Guid serverId);
        Task UpdateMatchAsync(Match match);
        Task<Match> GetMatchByIdAsync(int matchId);
        Task<TeamMatch> CreateTeamAsync(TeamMatch team);
        Task CreateMatchMapStatsAsync(int matchId, int mapNumber, string mapName);
        Task UpdateMatchMapStatsAsync(MapStats request);
        Task<MapStats> GetMatchMapStatsByMatchIdAsync(int matchId, int mapNumber);
        Task UpdateMatchMapPlayerStatsAsync(PlayerStats request);
        Task<PlayerStats?> GetMatchMapPlayerStatsAsync(int matchId, int mapId, string steamId);
        Task CreateMatchMapPlayerStatsAsync(int matchId, int mapId, string steamId);
        Task<IEnumerable<MatchResult>> GetPlayerMatchesBySteamIdAsync(string steamId);
        Task<IEnumerable<string>> GetPlayersByMatchIdAsync(int matchId);
        Task<IEnumerable<PlayerResults>> GetPlayersByMatchAndTeamIdAsync(int matchId, Guid teamId);
    }
}
