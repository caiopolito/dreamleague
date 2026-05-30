namespace dreamleague.common.Entities.Teams
{
    public class PlayerTeam
    {
        public Guid TeamId { get; set; }
        public string SteamId { get; set; }
        public string Name { get; set; }
        public bool IsCaptain { get; set; }
        public string Avatar { get; set; }
    }
}
