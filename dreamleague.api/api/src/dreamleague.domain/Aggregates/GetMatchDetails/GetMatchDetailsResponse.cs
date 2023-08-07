using dreamleague.domain.Entities.Get5;

namespace dreamleague.domain.Aggregates.GetMatchDetails
{
    public class GetMatchDetailsResponse
    {
        public string TeamOne { get; set; }
        public string TeamTwo { get; set; }
        public IEnumerable<PlayerResults> PlayersTeamOne { get; set; }
        public IEnumerable<PlayerResults> PlayersTeamTwo { get; set; }
    }
}
