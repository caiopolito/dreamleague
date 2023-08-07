using dreamleague.domain.Aggregates.DeleteTeam;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Team
{
    public interface IDeleteTeamService : IService<DeleteTeamRequest, DeleteTeamResponse>
    {
    }
}
