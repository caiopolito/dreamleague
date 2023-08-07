using dreamleague.domain.Aggregates.FinishMatch;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Match
{
    public interface IFinishMatchService : IService<FinishMatchRequest, FinishMatchResponse>
    {
    }
}
