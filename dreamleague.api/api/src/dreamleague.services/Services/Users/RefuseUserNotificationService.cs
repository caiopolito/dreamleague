using dreamleague.domain.Aggregates.RefuseUserNotification;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Users;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Users
{
    public class RefuseUserNotificationService : GenericService<RefuseUserNotificationRequest, RefuseUserNotificationResponse>, IRefuseUserNotificationService
    {
        private readonly IUnitOfWork unitOfWork;
        public RefuseUserNotificationService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<RefuseUserNotificationResponse> OnExecute(RefuseUserNotificationRequest request)
        {
            await unitOfWork.NotificationRepository.SetTeamNotificationAsRespondedAsync(request.SteamId, request.NotificationId);

            return new RefuseUserNotificationResponse { };   
        }
    }
}
