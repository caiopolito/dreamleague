namespace dreamleague.common.Entities.Teams
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PlayerTeam> Players { get; set; } = new List<PlayerTeam>();
        public int PlayersAmount { get => Players.Count(); }
    }
}
