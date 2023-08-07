namespace dreamleague.domain.Aggregates.RemovePlayerFromTeam
{
    public class RemovePlayerFromTeamRequest
    {
        public string SteamId { get; set; }
        public Guid TeamId { get; set; }
    }
}
