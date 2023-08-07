using dreamleague.domain.Aggregates.RegisterTeamToChampionship;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Championship;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Championships
{
    public class RegisterTeamToChampionshipService : GenericService<RegisterTeamToChampionshipRequest, RegisterTeamToChampionshipResponse>, IRegisterTeamToChampionshipService
    {
        private readonly IUnitOfWork unitOfWork;
        public RegisterTeamToChampionshipService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<RegisterTeamToChampionshipResponse> OnExecute(RegisterTeamToChampionshipRequest request)
        {
            await unitOfWork.ChampionshipRepository.RegisterTeamAsync(request.TeamId, request.ChampionshipId);

            return new RegisterTeamToChampionshipResponse { };
        }
    }
}
