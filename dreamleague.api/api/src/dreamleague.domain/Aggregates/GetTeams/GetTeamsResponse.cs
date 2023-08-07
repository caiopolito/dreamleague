using dreamleague.domain.Entities.Teams;

namespace dreamleague.domain.Aggregates.GetTeams
{
    public class GetTeamsResponse
    {
        public IEnumerable<Team> Teams { get; set; }
    }
}
