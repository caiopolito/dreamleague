using dreamleague.common.Entities.Get5;

namespace dreamleague.domain.Aggregates.GetUserMatches
{
    public class GetUserMatchesResponse
    {
        public IEnumerable<MatchResult> Matches { get; set; }
    }
}
