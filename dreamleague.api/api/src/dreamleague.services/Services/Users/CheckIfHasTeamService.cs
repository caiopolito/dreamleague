using dreamleague.domain.Aggregates.CheckIfHasTeam;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Users;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Users
{
    public class CheckIfHasTeamService : GenericService<CheckIfHasTeamRequest, CheckIfHasTeamResponse>, ICheckIfHasTeamService
    {
        private readonly IUnitOfWork unitOfWork;
        public CheckIfHasTeamService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }

        protected async override Task<CheckIfHasTeamResponse> OnExecute(CheckIfHasTeamRequest request)
        {
            return new CheckIfHasTeamResponse
            {
                HasTeam = await unitOfWork.PlayerRepository.CheckIfHasTeamAsync(request.SteamId, request.ChampionshipId)
            };
        }
    }
}
