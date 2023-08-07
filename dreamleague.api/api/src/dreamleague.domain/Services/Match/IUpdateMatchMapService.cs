using dreamleague.domain.Aggregates.UpdateMatchMap;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Match
{
    public interface IUpdateMatchMapService : IService<UpdateMatchMapRequest, UpdateMatchMapResponse>
    {
    }
}
