using dreamleague.domain.Aggregates.UpdateTeam;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Team;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Teams
{
    public class UpdateTeamService : GenericService<UpdateTeamRequest, UpdateTeamResponse>, IUpdateTeamService
    {
        private readonly IUnitOfWork unitOfWork;
        public UpdateTeamService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<UpdateTeamResponse> OnExecute(UpdateTeamRequest request)
        {
            return await unitOfWork.TeamRepository.UpdateTeamAsync(request);
        }
    }
}
