using dreamleague.domain.Aggregates.CreateTeam;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Team;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Teams
{
    public class CreateTeamService : GenericService<CreateTeamRequest, CreateTeamResponse>, ICreateTeamService
    {
        private readonly IUnitOfWork unitOfWork;
        public CreateTeamService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<CreateTeamResponse> OnExecute(CreateTeamRequest request)
        {
            var team = await unitOfWork.TeamRepository.CreateTeamAsync(request);

            var player = await unitOfWork.PlayerRepository.GetPlayerInfoBySteamIdAsync(request.SteamId);

            await unitOfWork.TeamRepository.InsertPlayerIntoTeamAsync(new common.Entities.Teams.PlayerTeam
            {
                SteamId = request.SteamId,
                Name = player.Name,
                IsCaptain = true
            }, team.Id);

            return team;
        }
    }
}
