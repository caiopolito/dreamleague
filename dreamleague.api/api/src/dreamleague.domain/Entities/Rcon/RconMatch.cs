using dreamleague.domain.Aggregates.CreateMatch;
using dreamleague.domain.Entities.Get5;

namespace dreamleague.domain.Entities.Rcon
{
    public class RconMatch
    {
        public string json_file_name { get => match_title + "_" + matchid; }
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

        public static implicit operator RconMatch(CreateMatchRequest request)
        {

            return new RconMatch
            {
                team1 = new TeamMatch
                {
                    tag = "T1",
                    players = request.Players.Take(1).ToDictionary(item => item.Value.SteamId, item => item.Value.Name)
                },
                team2 = new TeamMatch
                {
                    tag = "T2",
                    players = request.Players.TakeLast(1).ToDictionary(item => item.Value.SteamId, item => item.Value.Name)
                }
            };
        }
    }
}
