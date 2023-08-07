namespace dreamleague.domain.Aggregates.UpdateTeam
{
    public class UpdateTeamRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? SteamId { get; set; }
    }
}
