namespace dreamleague.domain.Aggregates.RegisterTeamToChampionship
{
    public class RegisterTeamToChampionshipRequest
    {
        public Guid TeamId { get; set; }
        public Guid ChampionshipId { get; set; }
    }
}
