using Dapper;
using dreamleague.common.Entities.Players;
using dreamleague.domain.Infrastructure;
using System.Data;

namespace dreamleague.infrastructure.Repositories.Players
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IDatabaseFactory databaseFactory;
        public PlayerRepository(IDatabaseFactory databaseFactory)
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
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<Player> GetPlayerInfoBySteamIdAsync(string steamId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                return await connection.QueryFirstOrDefaultAsync<Player>(PlayerRepositoryQueries.GetPlayerInfoBySteamId, new { steamId });
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
        public async Task<Player> InsertPlayerAsync(string steamId, string name, Uri avatar)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("steamId", steamId, DbType.AnsiString);
                parameters.Add("name", name, DbType.AnsiString);
                parameters.Add("avatar", avatar.AbsoluteUri, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<Player>(PlayerRepositoryQueries.InsertAndReturnFirstTimePlayerInfo, parameters);
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

        public async Task<Player> UpdatePlayerNameAsync(string steamId, string name, Uri avatar)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("steamId", steamId, DbType.AnsiString);
                parameters.Add("name", name, DbType.AnsiString);
                parameters.Add("avatar", avatar.AbsoluteUri, DbType.AnsiString);
                return await connection.QueryFirstOrDefaultAsync<Player>(PlayerRepositoryQueries.UpdateNameAndReturnPlayer, parameters);
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

        public async Task<IEnumerable<Player>> GetAllPlayersAsync(PlayerFilter filter = null)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    name = filter?.Name ?? "",
                    points = filter?.Points ?? 0,
                    notIn = filter?.NotIn ?? Array.Empty<string>()
                };


                return await connection.QueryAsync<Player>(PlayerRepositoryQueries.GetAllPlayers, parameters);
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

        public async Task UpdatePlayerAsync(Player player)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                await connection.ExecuteAsync(PlayerRepositoryQueries.UpdatePlayer,
                    new
                    {
                        player.SteamId,
                        player.Coins,
                        player.Points
                    });
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

        public async Task<bool> CheckIfHasTeamAsync(string steamId, Guid championshipId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                return await connection.QueryFirstOrDefaultAsync<bool>(PlayerRepositoryQueries.CheckIfHasTeam, new { SteamId = steamId, ChampionshipId = championshipId });
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
