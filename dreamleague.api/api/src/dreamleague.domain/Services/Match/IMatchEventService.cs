using dreamleague.domain.Aggregates.MatchEvent;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Match
{
    public interface IMatchEventService : IService<MatchEventRequest, object>
    {
    }
}
