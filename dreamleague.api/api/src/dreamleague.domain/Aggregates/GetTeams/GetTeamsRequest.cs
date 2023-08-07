namespace dreamleague.domain.Aggregates.GetTeams
{
    public class GetTeamsRequest
    {
        public string SteamId { get; set; }
        public bool? IsCaptain { get; set; }
    }
}
