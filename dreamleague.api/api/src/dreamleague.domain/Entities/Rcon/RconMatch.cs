using dreamleague.domain.Entities.Get5;

namespace dreamleague.domain.Entities.Rcon
{
    public class RconMatch
    {
        public RconMatch(Match match, TeamMatch team1, TeamMatch team2)
        {
            this.team1 = team1;
            this.team2 = team2;
            matchid = match.MatchId;
            server_id = match.ServerId;
        }
        public string match_title { get => "match_live"; }
        public string matchid { get; set; }
        public Guid server_id { get; set; }
        public bool clinch_series { get => true; }
        public int num_maps { get => 1; }
        public int players_per_team { get => 1; }
        public int coaches_per_team { get => 0; }
        public bool coaches_must_ready { get => false; }
        public int min_players_to_ready { get => 0; }
        public int min_spectators_to_ready { get => 0; }
        public bool skip_veto { get => true; }
        public string side_type { get => "always_knife"; }
        private List<string> maps = new List<string>
        {
            "de_ancient",
            "de_mirage",
            "de_inferno",
            "de_dust2",
        };
        public IEnumerable<string> maplist
        {
            get
            {
                Random rand = new Random();
                return new string[] { maps[rand.Next(0, 4)] };
            }
        }
        public List<string> map_sides { get; set; } = new List<string>();
        public TeamMatch team1 { get; set; }
        public TeamMatch team2 { get; set; }
    }
}
