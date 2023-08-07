using dreamleague.domain.Aggregates.RemovePlayerFromTeam;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Team
{
    public interface IRemovePlayerFromTeamService : IService<RemovePlayerFromTeamRequest, RemovePlayerFromTeamResponse>
    {
    }
}
