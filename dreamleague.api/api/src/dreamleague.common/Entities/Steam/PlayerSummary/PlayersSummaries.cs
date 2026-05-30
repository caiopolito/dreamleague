namespace dreamleague.common.Entities.Steam.PlayerSummary
{
    public class PlayersSummaries
    {
        public PlayersSummaries()
        {
            players = new();
        }
        public List<SteamPlayer> players { get; set; }
    }
}
