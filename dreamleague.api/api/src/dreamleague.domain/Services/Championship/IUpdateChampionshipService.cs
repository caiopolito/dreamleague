using dreamleague.domain.Aggregates.UpdateChampionship;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Championship
{
    public interface IUpdateChampionshipService : IService<UpdateChampionshipRequest, UpdateChampionshipResponse>
    {
    }
}
