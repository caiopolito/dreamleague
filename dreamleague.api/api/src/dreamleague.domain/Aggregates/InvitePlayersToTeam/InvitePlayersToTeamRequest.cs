namespace dreamleague.domain.Aggregates.InvitePlayersToTeam
{
    public class InvitePlayersToTeamRequest
    {
        public IEnumerable<string> SteamIds { get; set; }
        public Guid TeamId { get; set; }
    }
}
