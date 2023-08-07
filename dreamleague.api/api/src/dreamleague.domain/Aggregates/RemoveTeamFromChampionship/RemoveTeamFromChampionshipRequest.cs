namespace dreamleague.domain.Aggregates.RemoveTeamFromChampionship
{
    public class RemoveTeamFromChampionshipRequest
    {
        public Guid ChampionshipId { get; set; }
        public Guid TeamId { get; set; }
    }
}
