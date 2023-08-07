using dreamleague.domain.Aggregates.DeleteChampionship;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Championship
{
    public interface IDeleteChampionshipService : IService<DeleteChampionshipRequest, DeleteChampionshipResponse>
    {
    }
}
