using dreamleague.domain.Aggregates.GetChampionshipTeams;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Championship
{
    public interface IGetChampionshipTeamsService : IService<GetChampionshipTeamsRequest, GetChampionshipTeamsResponse>
    {
    }
}
