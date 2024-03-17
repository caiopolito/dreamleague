using dreamleague.common.Entities.Notifications;

namespace dreamleague.domain.Infrastructure
{
    public interface INotificationRepository : IDatabaseRepository
    {
        Task<IEnumerable<TeamNotification>> GetTeamNotificationsAsync(string steamId);
        Task<TeamNotification> GetTeamNotificationByIdAsync(Guid notificationId);
        Task<TeamNotification> InsertTeamNotificationAsync(string steamId, Guid teamId);
        Task SetTeamNotificationsAsSeenAsync(string steamId);
        Task SetTeamNotificationAsRespondedAsync(string steamId, Guid notificationId);
        Task DeleteNotificationsByTeamIdAsync(Guid teamId);
    }
}
