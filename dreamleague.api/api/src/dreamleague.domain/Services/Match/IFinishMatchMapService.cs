using dreamleague.domain.Aggregates.FinishMatchMap;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Match
{
    public interface IFinishMatchMapService : IService<FinishMatchMapRequest, FinishMatchMapResponse>
    {
    }
}
