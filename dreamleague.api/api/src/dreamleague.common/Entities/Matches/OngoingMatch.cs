using dreamleague.common.Entities.Rcon;
using dreamleague.common.Entities.Servers;

namespace dreamleague.common.Entities.Matches
{
    public class OngoingMatch
    {
        public ServerConnection Server { get; set; }
        public RconMatch Match { get; set; }
    }
}
