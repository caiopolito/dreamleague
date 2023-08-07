using dreamleague.domain.Aggregates.GetTeams;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Team
{
    public interface IGetTeamsService : IService<GetTeamsRequest, GetTeamsResponse>
    {
    }
}
