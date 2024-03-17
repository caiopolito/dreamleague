using Dapper;
using dreamleague.domain.Aggregates.CreateTeam;
using dreamleague.domain.Aggregates.DeleteTeam;
using dreamleague.domain.Aggregates.GetTeamById;
using dreamleague.domain.Aggregates.GetTeams;
using dreamleague.domain.Aggregates.UpdateTeam;
using dreamleague.common.Entities.Teams;
using dreamleague.domain.Infrastructure;

namespace dreamleague.infrastructure.Repositories.Teams
{
    public class TeamRepository : ITeamRepository
    {
        private readonly IDatabaseFactory databaseFactory;
        public TeamRepository(IDatabaseFactory databaseFactory)
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

        public async Task<Team> CreateTeamAsync(CreateTeamRequest request)
        {
                using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    request.Name
                };

                return await connection.QueryFirstOrDefaultAsync<Team>(TeamRepositoryQueries.InsertAndReturnTeam, param: parameters);
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

        public async Task<Team> UpdateTeamAsync(UpdateTeamRequest request)
        {
                using var connection = databaseFactory.GetConnection();
            try
            {
                return await connection.QueryFirstOrDefaultAsync<Team>(TeamRepositoryQueries.UpdateTeamById, new
                {
                    TeamId = request.Id,
                    request.Name
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

        public async Task DeleteTeamAsync(DeleteTeamRequest request)
        {
                using var connection = databaseFactory.GetConnection();
            try
            {
                await connection.ExecuteAsync(TeamRepositoryQueries.DeleteTeamById, new { request.TeamId });
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

        public async Task DeleteAllTeamPlayersAsync(Guid teamId)
        {
                using var connection = databaseFactory.GetConnection();
            try
            {
                await connection.ExecuteAsync(TeamRepositoryQueries.DeleteAllPlayersFromTeam, new { TeamId = teamId });
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

        public async Task<Team> GetTeamByIdAsync(GetTeamByIdRequest request)
        {
                using var connection = databaseFactory.GetConnection();
            try
            {
                return await connection.QueryFirstOrDefaultAsync<Team>(TeamRepositoryQueries.GetTeamById, new { request.TeamId });
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

        public async Task InsertPlayerIntoTeamAsync(PlayerTeam player, Guid teamId)
        {
                using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    TeamId = teamId,
                    PlayerId = player.SteamId,
                    player.IsCaptain
                };
                await connection.ExecuteAsync(TeamRepositoryQueries.InsertPlayerIntoTeam, parameters, commandTimeout:0);
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

        public async Task InsertPlayersIntoTeamAsync(IEnumerable<PlayerTeam> player, Guid teamId)
        {
                using var connection = databaseFactory.GetConnection();
            try
            {
                var param = player.Select(x => new { 
                    TeamId = teamId,
                    PlayerId = x.SteamId,
                    x.IsCaptain
                }).ToList();
                await connection.ExecuteAsync(TeamRepositoryQueries.InsertPlayerIntoTeam, param, commandTimeout: 0);
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

        public async Task<IEnumerable<PlayerTeam>> GetTeamPlayersAsync(Guid teamId)
        {
                using var connection = databaseFactory.GetConnection();
            try
            {
                return await connection.QueryAsync<PlayerTeam>(TeamRepositoryQueries.GetTeamPlayers, new { TeamId = teamId });
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

        public async Task<IEnumerable<Team>> GetTeamsAsync(GetTeamsRequest request)
        {
                using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    request.SteamId,
                    IsCaptain = request.IsCaptain is not null ? new bool[1] { request.IsCaptain.Value } : new bool[2] { true, false }
                };
                var teams = await connection.QueryAsync<Team>(TeamRepositoryQueries.GetTeams, parameters);


                foreach (var item in teams)
                {
                    item.Players = await connection.QueryAsync<PlayerTeam>(TeamRepositoryQueries.GetTeamPlayers, new { TeamId = item.Id });
                }   

                return teams;
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

        public async Task RemovePlayerAsync(string steamId, Guid teamId)
        {
                using var connection = databaseFactory.GetConnection();
            try
            {
                await connection.ExecuteAsync(TeamRepositoryQueries.DeletePlayer, new { TeamId = teamId, SteamId = steamId });
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
