using dreamleague.domain.Aggregates.StartMatchMap;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Match
{
    public interface IStartMatchMapService : IService<StartMatchMapRequest, StartMatchMapResponse>
    {
    }
}
