using dreamleague.domain.Aggregates.RemovePlayerFromTeam;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Team;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Teams
{
    public class RemovePlayerFromTeamService : GenericService<RemovePlayerFromTeamRequest, RemovePlayerFromTeamResponse>, IRemovePlayerFromTeamService
    {
        private readonly IUnitOfWork unitOfWork;
        public RemovePlayerFromTeamService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<RemovePlayerFromTeamResponse> OnExecute(RemovePlayerFromTeamRequest request)
        {
            await unitOfWork.TeamRepository.RemovePlayerAsync(request.SteamId, request.TeamId);

            return new RemovePlayerFromTeamResponse { };
        }
    }
}
