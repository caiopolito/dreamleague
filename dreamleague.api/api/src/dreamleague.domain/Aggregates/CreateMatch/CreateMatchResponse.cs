using dreamleague.common.Entities.Rcon;
using dreamleague.common.Entities.Servers;

namespace dreamleague.domain.Aggregates.CreateMatch
{
    public class CreateMatchResponse
    {
        public ServerConnection Server { get; set; }
        public RconMatch Match { get; set; }
    }
}
