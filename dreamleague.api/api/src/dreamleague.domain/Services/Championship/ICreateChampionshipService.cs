using dreamleague.domain.Aggregates.CreateChampionship;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Championship
{
    public interface ICreateChampionshipService : IService<CreateChampionshipRequest, CreateChampionshipResponse>
    {
    }
}
