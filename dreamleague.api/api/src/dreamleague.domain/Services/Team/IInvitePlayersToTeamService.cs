using dreamleague.domain.Aggregates.InvitePlayersToTeam;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Team
{
    public interface IInvitePlayersToTeamService : IService<InvitePlayersToTeamRequest, InvitePlayersToTeamResponse>
    {
    }
}
