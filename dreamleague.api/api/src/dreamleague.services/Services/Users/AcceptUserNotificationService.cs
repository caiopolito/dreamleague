using dreamleague.domain.Aggregates.AcceptUserNotification;
using dreamleague.domain.Entities.Teams;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Users;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Users
{
    public class AcceptUserNotificationService : GenericService<AcceptUserNotificationRequest, AcceptUserNotificationResponse>, IAcceptUserNotificationService
    {
        private readonly IUnitOfWork unitOfWork;
        public AcceptUserNotificationService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<AcceptUserNotificationResponse> OnExecute(AcceptUserNotificationRequest request)
        {
            var player = await unitOfWork.PlayerRepository.GetPlayerInfoBySteamIdAsync(request.SteamId);

            var notification = await unitOfWork.NotificationRepository.GetTeamNotificationByIdAsync(request.NotificationId);

            await unitOfWork.TeamRepository.InsertPlayerIntoTeamAsync(new PlayerTeam
            {
                SteamId = request.SteamId,
                Name = player.Name,
                IsCaptain = false
            }, notification.TeamId);

            await unitOfWork.NotificationRepository.SetTeamNotificationAsRespondedAsync(request.SteamId, request.NotificationId);

            return new AcceptUserNotificationResponse { };
        }
    }
}
