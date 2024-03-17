using dreamleague.domain.Aggregates.CreateMatch;
using dreamleague.common.Entities.Get5;
using dreamleague.common.Entities.Players;
using dreamleague.common.Entities.Rcon;
using dreamleague.common.Entities.Servers;

namespace dreamleague.domain.Adapters.CreateMatch
{
    public interface ICreateMatchAdapter
    {
        TeamMatch ToTeamMatch(IEnumerable<KeyValuePair<string, Player>> players);
        CreateMatchResponse ToCreateMatchResponse(Server server, RconMatch rconMatch);        
        Match ToGet5Match(CreateMatchRequest request);
    }
}
