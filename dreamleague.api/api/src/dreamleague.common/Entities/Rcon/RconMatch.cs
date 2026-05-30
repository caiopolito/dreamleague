using dreamleague.common.Entities.Get5;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace dreamleague.common.Entities.Rcon
{
    public class RconMatch
    {
        public RconMatch(Match match, TeamMatch team1, TeamMatch team2)
        {
            this.team1 = team1;
            this.team2 = team2;
            this.matchid = match.MatchId;
            this.server_id = match.ServerId;

            List<string> maps = new List<string>
            {
                "de_ancient",
                "de_mirage",
                "de_inferno",
                "de_dust2",
            };
            Random rand = new Random();
            this.maplist = new string[] { maps[rand.Next(0, 4)] };
        }
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("match_title")]
        public string match_title { get; set; } = "match_live";
        [BsonElement("matchid")]
        public string matchid { get; set; }
        [BsonElement("server_id")]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid server_id { get; set; }
        [BsonElement("clinch_series")]
        public bool clinch_series { get; set; } = true;
        [BsonElement("num_maps")]
        public int num_maps { get; set; } = 1;
        [BsonElement("players_per_team")]
        public int players_per_team { get; set; } = 1;
        [BsonElement("coaches_per_team")]
        public int coaches_per_team { get; set; } = 0;
        [BsonElement("coaches_must_ready")]
        public bool coaches_must_ready { get; set; } = false;
        [BsonElement("min_players_to_ready")]
        public int min_players_to_ready { get; set; } = 0;
        [BsonElement("min_spectators_to_ready")]
        public int min_spectators_to_ready { get; set; } = 0;
        [BsonElement("skip_veto")]
        public bool skip_veto { get; set; } = true;
        [BsonElement("side_type")]
        public string side_type { get; set; } = "always_knife";
        [BsonElement("maplist")]
        public IEnumerable<string> maplist { get; set; }
        [BsonElement("map_sides")]
        public List<string> map_sides { get; set; } = new List<string> { "knife" };
        [BsonElement("team1")]
        public TeamMatch team1 { get; set; }
        [BsonElement("team2")]
        public TeamMatch team2 { get; set; }
    }
}
