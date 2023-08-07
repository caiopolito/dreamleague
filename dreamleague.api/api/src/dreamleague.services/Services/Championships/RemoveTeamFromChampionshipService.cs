using dreamleague.domain.Aggregates.RemoveTeamFromChampionship;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Championship;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Championships
{
    public class RemoveTeamFromChampionshipService : GenericService<RemoveTeamFromChampionshipRequest, RemoveTeamFromChampionshipResponse>, IRemoveTeamFromChampionshipService
    {
        private readonly IUnitOfWork unitOfWork;
        public RemoveTeamFromChampionshipService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected override async Task<RemoveTeamFromChampionshipResponse> OnExecute(RemoveTeamFromChampionshipRequest request)
        {
            await unitOfWork.ChampionshipRepository.RemoveTeamAsync(request.TeamId, request.ChampionshipId);

            return new RemoveTeamFromChampionshipResponse();
        }
    }
}
