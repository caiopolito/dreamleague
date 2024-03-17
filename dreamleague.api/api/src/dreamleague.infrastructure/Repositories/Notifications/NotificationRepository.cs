using Dapper;
using dreamleague.common.Entities.Notifications;
using dreamleague.domain.Infrastructure;

namespace dreamleague.infrastructure.Repositories.Notifications
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDatabaseFactory databaseFactory;
        public NotificationRepository
            (
                IDatabaseFactory databaseFactory
            )
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

        public async Task<IEnumerable<TeamNotification>> GetTeamNotificationsAsync(string steamId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                return await connection.QueryAsync<TeamNotification>(NotificationRepositoryQueries.GetTeamNotifications, new { SteamId = steamId });
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

        public async Task<TeamNotification> InsertTeamNotificationAsync(string steamId, Guid teamId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                return await connection.QueryFirstOrDefaultAsync<TeamNotification>(NotificationRepositoryQueries.CreateTeamNotification, new { SteamId = steamId, TeamId = teamId });


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

        public async Task SetTeamNotificationsAsSeenAsync(string steamId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                await connection.ExecuteAsync(NotificationRepositoryQueries.SetTeamNotificationsAsSeen, new { SteamId = steamId });
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

        public async Task SetTeamNotificationAsRespondedAsync(string steamId, Guid notificationId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                await connection.ExecuteAsync(NotificationRepositoryQueries.SetTeamNotificationAsResponded, new { SteamId = steamId, NotificationId = notificationId });

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

        public async Task<TeamNotification> GetTeamNotificationByIdAsync(Guid notificationId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                return await connection.QueryFirstOrDefaultAsync<TeamNotification>(NotificationRepositoryQueries.GetTeamNotificationById, new { NotificationId = notificationId });
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

        public async Task DeleteNotificationsByTeamIdAsync(Guid teamId)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                await connection.ExecuteAsync(NotificationRepositoryQueries.DeleteNotificationsByTeamId, new { TeamId = teamId });
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
