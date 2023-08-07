using dreamleague.domain.Aggregates.GetUserNotifications;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Users;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Users
{
    public class GetUserNotificationsService: GenericService<GetUserNotificationsRequest, GetUserNotificationsResponse>, IGetUserNotificationsService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetUserNotificationsService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected override async Task<GetUserNotificationsResponse> OnExecute(GetUserNotificationsRequest request)
        {
            return new GetUserNotificationsResponse
            {
                TeamNotifications = await unitOfWork.NotificationRepository.GetTeamNotificationsAsync(request.SteamId)
            };
        }
    }
}
