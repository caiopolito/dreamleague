using dreamleague.domain.Aggregates.RefuseUserNotification;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Users
{
    public interface IRefuseUserNotificationService :IService<RefuseUserNotificationRequest, RefuseUserNotificationResponse>
    {
    }
}
