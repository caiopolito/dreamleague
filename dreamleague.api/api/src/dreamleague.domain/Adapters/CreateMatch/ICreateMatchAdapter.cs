using dreamleague.domain.Aggregates.CreateMatch;
using dreamleague.domain.Entities.Get5;
using dreamleague.domain.Entities.Players;
using dreamleague.domain.Entities.Rcon;
using dreamleague.domain.Entities.Servers;

namespace dreamleague.domain.Adapters.CreateMatch
{
    public interface ICreateMatchAdapter
    {
        TeamMatch ToTeamMatch(IEnumerable<KeyValuePair<string, Player>> players);
        CreateMatchResponse ToCreateMatchResponse(Server server, RconMatch rconMatch);
    }
}
