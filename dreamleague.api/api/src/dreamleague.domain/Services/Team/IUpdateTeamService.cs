using dreamleague.domain.Aggregates.UpdateTeam;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Team
{
    public interface IUpdateTeamService : IService<UpdateTeamRequest, UpdateTeamResponse>
    {
    }
}
