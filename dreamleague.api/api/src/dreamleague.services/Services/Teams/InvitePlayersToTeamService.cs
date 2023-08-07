using dreamleague.domain.Aggregates.InvitePlayersToTeam;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Team;
using dreamleague.hubs.Hubs;
using dreamleague.shared.Services;
using Microsoft.AspNetCore.SignalR;

namespace dreamleague.services.Services.Teams
{
    public class InvitePlayersToTeamService : GenericService<InvitePlayersToTeamRequest, InvitePlayersToTeamResponse>, IInvitePlayersToTeamService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHubContext<NotificationHub> hubContext;
        public InvitePlayersToTeamService
            (
                IUnitOfWork unitOfWork,
                IHubContext<NotificationHub> hubContext
            )
        {
            this.unitOfWork = unitOfWork;
            this.hubContext = hubContext;
        }
        protected async override Task<InvitePlayersToTeamResponse> OnExecute(InvitePlayersToTeamRequest request)
        {
            foreach (var item in request.SteamIds)
            {
                await unitOfWork.NotificationRepository.InsertTeamNotificationAsync(item, request.TeamId);
            }

            await hubContext.Clients.Group("notifications").SendAsync("UpdateNotifications");

            return new InvitePlayersToTeamResponse();
        }
    }
}
