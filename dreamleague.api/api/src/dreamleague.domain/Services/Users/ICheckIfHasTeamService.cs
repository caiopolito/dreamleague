using dreamleague.domain.Aggregates.CheckIfHasTeam;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Users
{
    public interface ICheckIfHasTeamService : IService<CheckIfHasTeamRequest, CheckIfHasTeamResponse>
    {
    }
}
