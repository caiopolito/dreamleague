using dreamleague.common.Entities.Teams;

namespace dreamleague.domain.Aggregates.GetChampionshipTeams
{
    public class GetChampionshipTeamsResponse
    {
        public IEnumerable<Team> Teams { get; set; }
    }
}
