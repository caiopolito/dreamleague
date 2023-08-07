using dreamleague.domain.Aggregates.AcceptUserNotification;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Users
{
    public interface  IAcceptUserNotificationService : IService<AcceptUserNotificationRequest, AcceptUserNotificationResponse>
    {
    }
}
