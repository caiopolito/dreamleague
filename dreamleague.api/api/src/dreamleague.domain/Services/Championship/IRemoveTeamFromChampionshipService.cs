using dreamleague.domain.Aggregates.RemoveTeamFromChampionship;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Championship
{
    public interface IRemoveTeamFromChampionshipService : IService<RemoveTeamFromChampionshipRequest, RemoveTeamFromChampionshipResponse>
    {
    }
}
