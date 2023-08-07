using dreamleague.domain.Entities.Rcon;
using dreamleague.domain.Entities.Servers;

namespace dreamleague.domain.Aggregates.CreateMatch
{
    public class CreateMatchResponse
    {
        public ServerConnection Server { get; set; }
        public RconMatch Match { get; set; }
    }
}
