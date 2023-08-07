using dreamleague.domain.Aggregates.CreateTeam;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Team
{
    public interface ICreateTeamService : IService<CreateTeamRequest, CreateTeamResponse>
    {
    }
}
