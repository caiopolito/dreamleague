using Azure.Core;
using Dapper;
using dreamleague.domain.Aggregates.CreateChampionship;
using dreamleague.domain.Aggregates.DeleteChampionship;
using dreamleague.domain.Aggregates.GetChampionshipById;
using dreamleague.domain.Aggregates.GetChampionships;
using dreamleague.domain.Aggregates.UpdateChampionship;
using dreamleague.domain.Entities.Championships;
using dreamleague.domain.Entities.Teams;
using dreamleague.domain.Infrastructure;
using dreamleague.infrastructure.Repositories.Teams;
using System.Transactions;

namespace dreamleague.infrastructure.Repositories.Championships
{
    public class ChampionshipRepository : IChampionshipRepository
    {
        private readonly IDatabaseFactory databaseFactory;
        public ChampionshipRepository(IDatabaseFactory databaseFactory)
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

        public async Task<Championship> CreateChampionshipAsync(CreateChampionshipRequest request)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    request.Description,
                    request.StartDate,
                    request.Name,
                    request.MinTeams,
                    request.PlayersOnTeam
                };

                return await connection.QueryFirstOrDefaultAsync<Championship>(ChampionshipRepositoryQueries.InsertAndReturnChampionship, param: parameters);
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

        public async Task<IEnumerable<Championship>> GetChampionshipsAsync(GetChampionshipsRequest request)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {

                return await connection.QueryAsync<Championship>(ChampionshipRepositoryQueries.GetAvailableChampionships);
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

        public async Task DeleteChampionshipAsync(DeleteChampionshipRequest request)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                await connection.ExecuteAsync(ChampionshipRepositoryQueries.DeleteChampionshipById, new { request.ChampionshipId });
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

        public async Task<Championship> UpdateChampionshipAsync(UpdateChampionshipRequest request)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    ChampionshipId = request.Id,
                    request.Description,
                    request.StartDate,
                    request.Name,
                    request.MinTeams,
                    request.PlayersOnTeam
                };
                return await connection.QueryFirstOrDefaultAsync<Championship>(ChampionshipRepositoryQueries.UpdateChampionshipById, parameters);
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

        public async Task<Championship> GetChampionshipByIdAsync(GetChampionshipByIdRequest request)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                return await connection.QueryFirstOrDefaultAsync<Championship>(ChampionshipRepositoryQueries.GetChampionshipById, new { request.ChampionshipId });
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

        public async Task RegisterTeamAsync(Guid teamId, Guid championshipId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                await connection.ExecuteAsync(ChampionshipRepositoryQueries.RegisterTeam, new { ChampionshipId = championshipId, TeamId = teamId });
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

        public async Task<IEnumerable<Team>> GetChampionshipTeamsAsync(Guid championshipId)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            using var connection = databaseFactory.GetConnection();
            try
            {
                var teams = await connection.QueryAsync<Team>(ChampionshipRepositoryQueries.GetChampionshipTeams, new { ChampionshipId = championshipId });

                foreach (var item in teams)
                {
                    item.Players = await connection.QueryAsync<PlayerTeam>(TeamRepositoryQueries.GetTeamPlayers, new { TeamId = item.Id });
                }

                transactionScope.Complete();
                return teams;
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

        public async Task RemoveTeamAsync(Guid teamId, Guid championshipId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                await connection.ExecuteAsync(ChampionshipRepositoryQueries.RemoveTeam, new { ChampionshipId = championshipId, TeamId = teamId });
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
