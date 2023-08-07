using dreamleague.domain.Aggregates.SetUserNotificationsAsSeen;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Users
{
    public interface ISetUserNotificationsAsSeenService : IService<SetUserNotificationsAsSeenRequest, SetUserNotificationsAsSeenResponse>
    {
    }
}
