using dreamleague.domain.Aggregates.CreateMatch;
using dreamleague.domain.Entities.Get5;
using dreamleague.domain.Entities.Players;
using dreamleague.domain.Entities.Rcon;
using dreamleague.domain.Entities.Servers;

namespace dreamleague.domain.Adapters.CreateMatch
{
    public class CreateMatchAdapter : ICreateMatchAdapter
    {

        public TeamMatch ToTeamMatch(IEnumerable<KeyValuePair<string, Player>> players)
        {
            return new TeamMatch
            {
                name = "team " + players.First().Value.Name,
                tag = $"T{players.First().Value.Name.Substring(1,1)}",
                players = players.ToDictionary(item => item.Value.SteamId, item => item.Value.Name)
            };
        }
        public CreateMatchResponse ToCreateMatchResponse(Server server, RconMatch rconMatch)
        {
            return new CreateMatchResponse
            {
                Server = new ServerConnection
                {
                    IpAddress = server.IpAddress,
                    Port = server.Port,
                    Password = server.Password,
                },
                Match = rconMatch
            };
        }

    }
}
