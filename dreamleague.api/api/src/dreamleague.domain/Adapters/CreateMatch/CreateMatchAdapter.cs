using dreamleague.domain.Aggregates.CreateMatch;
using dreamleague.common.Entities.Get5;
using dreamleague.common.Entities.Players;
using dreamleague.common.Entities.Rcon;
using dreamleague.common.Entities.Servers;
using SharpCompress.Common;

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

        public Match ToGet5Match(CreateMatchRequest request)
        {
            return new Match
            {
                Team1Id = request.Team1Id.Value,
                Team2Id = request.Team2Id.Value,
                Team1String = request.Team1String,
                Team2String = request.Team2String,
                ApiKey = request.ApiKey.ToString()
            };
        }
    }
}
