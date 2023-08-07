using dreamleague.domain.Aggregates.GetTeams;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Team;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Teams
{
    public class GetTeamsService : GenericService<GetTeamsRequest, GetTeamsResponse>, IGetTeamsService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetTeamsService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<GetTeamsResponse> OnExecute(GetTeamsRequest request)
        {
            return new GetTeamsResponse
            {
                Teams = await unitOfWork.TeamRepository.GetTeamsAsync(request)
            };
        }
    }
}
