using Dapper;
using dreamleague.domain.Entities.Get5;
using dreamleague.domain.Infrastructure;
using System.Transactions;

namespace dreamleague.infrastructure.Repositories.Matches
{
    public class MatchRepository : IMatchRepository
    {
        private readonly IDatabaseFactory databaseFactory;
        public MatchRepository(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }
        public async Task<bool> GetHealthCheckAsync()
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                return await connection.QueryFirstOrDefaultAsync<bool>(@"SELECT 1");
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<Match> CreateMatchAsync(Match match, Guid serverId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var inserted = await connection.QueryFirstOrDefaultAsync<Match>(MatchRepositoryQueries.CreateMatch, new { match.Team1Id, match.Team2Id, match.MatchTitle, match.ApiKey, ServerId = serverId, match.Team1String, match.Team2String });

                match.MatchId = inserted.MatchId;
                match.ServerId = inserted.ServerId;
                return match;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

        }

        public async Task<TeamMatch> CreateTeamAsync(TeamMatch team)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    Name = team.name,
                    Flag = team.flag,
                    Tag = team.tag,
                    Logo = team.logo

                };
                var inserted = await connection.QueryFirstOrDefaultAsync<TeamMatch>(MatchRepositoryQueries.CreateTeam, parameters);

                foreach (var item in team.players)
                {
                    await connection.ExecuteAsync(MatchRepositoryQueries.CreatePlayerTeam, new { TeamId = inserted.id, PlayerId = item.Key });
                }

                transactionScope.Complete();
                return inserted;
            }
            catch (Exception)
            {
                transactionScope.Dispose();
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task UpdateMatchAsync(Match match)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    match.MatchId,
                    match.StartTime,
                    EndTime = match.EndTime ?? null,
                    Forfeit = match.Forfeit ?? false,
                    Cancelled = match.Cancelled ?? false,
                    Team1Score = match.Team1Score ?? 0,
                    Team2Score = match.Team2Score ?? 0,
                    Winner = match.Winner ?? null
                };
                await connection.ExecuteAsync(MatchRepositoryQueries.UpdateMatch, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }


        public async Task<Match> GetMatchByIdAsync(int matchId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    MatchId = matchId
                };
                return await connection.QueryFirstOrDefaultAsync<Match>(MatchRepositoryQueries.GetMatchById, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task CreateMatchMapStatsAsync(int matchId, int mapNumber, string mapName)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    MatchId = matchId,
                    MapNumber = mapNumber,
                    MapName = mapName
                };
                await connection.ExecuteAsync(MatchRepositoryQueries.CreateMatchMapStats, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<MapStats> GetMatchMapStatsByMatchIdAsync(int matchId, int mapNumber)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    MatchId = matchId,
                    MapNumber = mapNumber
                };
                return await connection.QueryFirstOrDefaultAsync<MapStats>(MatchRepositoryQueries.GetMatchMapStatsByMatchId, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task UpdateMatchMapStatsAsync(MapStats request)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    request.MatchId,
                    request.MapNumber,
                    request.Team1Score,
                    request.Team2Score,
                    Winner = request.Winner ?? null,
                    EndTime = request.EndTime ?? null
                };
                await connection.ExecuteAsync(MatchRepositoryQueries.UpdateMatchMapStats, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
        public async Task<PlayerStats?> GetMatchMapPlayerStatsAsync(int matchId, int mapId, string steamId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    MatchId = matchId,
                    MapId = mapId,
                    SteamId = steamId
                };
                return await connection.QueryFirstOrDefaultAsync<PlayerStats>(MatchRepositoryQueries.GetMatchMapPlayerStats, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

        }

        public async Task UpdateMatchMapPlayerStatsAsync(PlayerStats request)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    request.MatchId,
                    request.MapId,
                    request.SteamId,
                    request.Name,
                    request.TeamId,
                    request.Kills,
                    request.Deaths,
                    request.Assists,
                    request.FlashbangAssists,
                    request.Teamkills,
                    request.Suicides,
                    request.Damage,
                    request.HeadshotKill,
                    request.RoundsPlayed,
                    request.BombPlants,
                    request.BombDefuses,
                    request.k1,
                    request.k2,
                    request.k3,
                    request.k4,
                    request.k5,
                    request.v1,
                    request.v2,
                    request.v3,
                    request.v4,
                    request.v5,
                    request.FirstKillT,
                    request.FirstKillCt,
                    request.FirstDeathT,
                    request.FirstDeathCt
                };
                await connection.ExecuteAsync(MatchRepositoryQueries.UpdateMatchMapPlayerStats, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task CreateMatchMapPlayerStatsAsync(int matchId, int mapId, string steamId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    MatchId = matchId,
                    MapId = mapId,
                    SteamId = steamId
                };
                await connection.ExecuteAsync(MatchRepositoryQueries.CreateMatchMapPlayerStats, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<IEnumerable<MatchResult>> GetPlayerMatchesBySteamIdAsync(string steamId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    SteamId = steamId
                };
                return await connection.QueryAsync<MatchResult>(MatchRepositoryQueries.GetPlayerMatchesBySteamId, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<IEnumerable<string>> GetPlayersByMatchIdAsync(int matchId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    MatchId = matchId
                };
                return await connection.QueryAsync<string>(MatchRepositoryQueries.GetPlayersByMatchId, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<IEnumerable<PlayerResults>> GetPlayersByMatchAndTeamIdAsync(int matchId, int teamId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    MatchId = matchId,
                    TeamId = teamId
                };
                return await connection.QueryAsync<PlayerResults>(MatchRepositoryQueries.GetPlayersByMatchAndTeamId, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
