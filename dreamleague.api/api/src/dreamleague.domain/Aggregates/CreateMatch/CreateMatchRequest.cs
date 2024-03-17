using dreamleague.common.Entities.Players;

namespace dreamleague.domain.Aggregates.CreateMatch
{
    public class CreateMatchRequest
    {
        private readonly Random rand = new Random();
        public IDictionary<string, Player> Players { get; set; }
        public Guid? Team1Id { get; set; }
        public string? Team1String { get; set; }
        public Guid? Team2Id { get; set; }
        public string? Team2String { get; set; }
        public int ApiKey { get => rand.Next(); }
    }
}
