using dreamleague.domain.Entities.Get5;

namespace dreamleague.domain.Entities.Rcon
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
        public string match_title { get; set; } = "match_live";
        public string matchid { get; set; }
        public Guid server_id { get; set; }
        public bool clinch_series { get => true; }
        public int num_maps { get; set; } = 1;
        public int players_per_team { get; set; } = 1;
        public int coaches_per_team { get; set; } = 0;
        public bool coaches_must_ready { get; set; } = false;
        public int min_players_to_ready { get; set; } = 0;
        public int min_spectators_to_ready { get; set; } = 0;
        public bool skip_veto { get; set; } = true;
        public string side_type { get; set; } = "always_knife";
        public IEnumerable<string> maplist { get; set; }
        public List<string> map_sides { get; set; } = new List<string>();
        public TeamMatch team1 { get; set; }
        public TeamMatch team2 { get; set; }
    }
}
