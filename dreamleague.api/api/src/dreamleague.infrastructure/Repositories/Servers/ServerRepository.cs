using Azure.Core;
using Dapper;
using dreamleague.domain.Entities.Chat;
using dreamleague.domain.Entities.Servers;
using dreamleague.domain.Infrastructure;
using dreamleague.infrastructure.Repositories.Chats;

namespace dreamleague.infrastructure.Repositories.Servers
{
    public class ServerRepository : IServerRepository
    {
        private readonly IDatabaseFactory databaseFactory;
        public ServerRepository(IDatabaseFactory databaseFactory)
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

        public async Task<Server> GetFirstAvailableServerAsync()
        {
                using var connection = databaseFactory.GetConnection();
            try
            {
                return await connection.QueryFirstOrDefaultAsync<Server>(ServerRepositoryQueries.GetAvailableServers);
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

        public async Task UpdateServerStatusAsync(Guid serverId, bool status)
        {
                using var connection = databaseFactory.GetConnection();
            try
            {
                await connection.ExecuteAsync(ServerRepositoryQueries.UpdateServerStatus, new { ServerId = serverId, Status = status });
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

        public async Task<Server> GetServerByIdAsync(Guid serverId)
        {
                using var connection = databaseFactory.GetConnection();
            try
            {
                return await connection.QueryFirstOrDefaultAsync<Server>(ServerRepositoryQueries.GetServerById, new { ServerId = serverId });
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
