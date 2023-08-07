namespace dreamleague.domain.Aggregates.CheckIfHasTeam
{
    public class CheckIfHasTeamRequest
    {
        public string SteamId { get; set; }
        public Guid ChampionshipId { get; set; }
    }
}
