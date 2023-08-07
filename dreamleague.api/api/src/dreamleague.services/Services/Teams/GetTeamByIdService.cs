using dreamleague.domain.Aggregates.GetTeamById;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Team;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Teams
{
    public class GetTeamByIdService : GenericService<GetTeamByIdRequest, GetTeamByIdResponse>, IGetTeamByIdService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetTeamByIdService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<GetTeamByIdResponse> OnExecute(GetTeamByIdRequest request)
        {
            var team = await unitOfWork.TeamRepository.GetTeamByIdAsync(request);

            return new GetTeamByIdResponse
            {
                Id = team.Id,
                Name = team.Name,
                Players = await unitOfWork.TeamRepository.GetTeamPlayersAsync(team.Id)
            };
        }
    }
}
