using dreamleague.domain.Entities.Rcon;
using dreamleague.domain.Entities.Servers;

namespace dreamleague.domain.Entities.Matches
{
    public class OngoingMatch
    {
        public ServerConnection Server { get; set; }
        public RconMatch Match { get; set; }
    }
}
