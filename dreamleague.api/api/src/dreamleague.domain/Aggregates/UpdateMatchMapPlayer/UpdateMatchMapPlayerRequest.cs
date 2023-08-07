namespace dreamleague.domain.Aggregates.UpdateMatchMapPlayer
{
    public class UpdateMatchMapPlayerRequest
    {
        // Route parameters
        public int MatchId { get; set; }
        public int MapNumber { get; set; }
        public string? SteamId { get; set; }

        // Form parameters
        public string key { get; set; }
        public string team { get; set; }
        public string name { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }
        public int assists { get; set; }
        public int flashbang_assists { get; set; }
        public int teamkills { get; set; }
        public int suicides { get; set; }
        public int damage { get; set; }
        public int util_damage { get; set; }
        public int enemies_flashed { get; set; }
        public int friendlies_flashed { get; set; }
        public int knife_kills { get; set; }
        public int headshot_kills { get; set; }
        public int roundsplayed { get; set; }
        public int bomb_plants { get; set; }
        public int bomb_defuses { get; set; }
        public int k1 { get; set; }
        public int k2 { get; set; }
        public int k3 { get; set; }
        public int k4 { get; set; }
        public int k5 { get; set; }
        public int v1 { get; set; }
        public int v2 { get; set; }
        public int v3 { get; set; }
        public int v4 { get; set; }
        public int v5 { get; set; }
        public int firstkill_t { get; set; }
        public int firstkill_ct { get; set; }
        public int firstdeath_t { get; set; }
        public int firstdeath_ct { get; set; }
        public int tradekill { get; set; }
        public int kast { get; set; }
        public int contribution_score { get; set; }
        public int mvp { get; set; }
    }
}
