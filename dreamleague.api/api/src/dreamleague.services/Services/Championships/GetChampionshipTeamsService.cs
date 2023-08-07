using dreamleague.domain.Aggregates.GetChampionshipTeams;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Championship;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Championships
{
    public class GetChampionshipTeamsService : GenericService<GetChampionshipTeamsRequest, GetChampionshipTeamsResponse>, IGetChampionshipTeamsService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetChampionshipTeamsService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<GetChampionshipTeamsResponse> OnExecute(GetChampionshipTeamsRequest request)
        {
            return new GetChampionshipTeamsResponse 
            { 
                Teams = await unitOfWork.ChampionshipRepository.GetChampionshipTeamsAsync(request.ChampionshipId)
            };
        }
    }
}
