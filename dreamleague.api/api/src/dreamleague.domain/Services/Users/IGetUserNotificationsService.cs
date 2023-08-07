using dreamleague.domain.Aggregates.GetUserNotifications;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Users
{
    public interface IGetUserNotificationsService : IService<GetUserNotificationsRequest, GetUserNotificationsResponse>
    {
    }
}
