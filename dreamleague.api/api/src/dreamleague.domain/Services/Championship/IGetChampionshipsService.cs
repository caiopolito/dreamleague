using dreamleague.domain.Aggregates.GetChampionships;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Championship
{
    public interface IGetChampionshipsService : IService<GetChampionshipsRequest, GetChampionshipsResponse>
    {
    }
}
