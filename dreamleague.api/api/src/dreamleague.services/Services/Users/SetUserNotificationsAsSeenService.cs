using dreamleague.domain.Aggregates.SetUserNotificationsAsSeen;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Users;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Users
{
    public class SetUserNotificationsAsSeenService : GenericService<SetUserNotificationsAsSeenRequest, SetUserNotificationsAsSeenResponse>, ISetUserNotificationsAsSeenService
    {
        private readonly IUnitOfWork unitOfWork;
        public SetUserNotificationsAsSeenService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<SetUserNotificationsAsSeenResponse> OnExecute(SetUserNotificationsAsSeenRequest request)
        {
            await unitOfWork.NotificationRepository.SetTeamNotificationsAsSeenAsync(request.SteamId);
            return new SetUserNotificationsAsSeenResponse { };
        }
    }
}
