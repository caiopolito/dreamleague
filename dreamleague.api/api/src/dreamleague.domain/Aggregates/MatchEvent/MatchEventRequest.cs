using System.Text.Json.Serialization;

namespace dreamleague.domain.Aggregates.MatchEvent
{
    public enum MatchZyEventType
    {
        Unknown,
        SeriesStart,
        SeriesEnd,
        GoingLive,
        MapResult,
        RoundEnd,
        DemoUploadEnded,
        MapPicked,
        MapVetoed,
        SidePicked,
    }

    public static class MatchZyEventTypeParser
    {
        public static MatchZyEventType Parse(string? eventName) => eventName switch
        {
            "series_start"        => MatchZyEventType.SeriesStart,
            "series_end"          => MatchZyEventType.SeriesEnd,
            "going_live"          => MatchZyEventType.GoingLive,
            "map_result"          => MatchZyEventType.MapResult,
            "round_end"           => MatchZyEventType.RoundEnd,
            "demo_upload_ended"   => MatchZyEventType.DemoUploadEnded,
            "map_picked"          => MatchZyEventType.MapPicked,
            "map_vetoed"          => MatchZyEventType.MapVetoed,
            "side_picked"         => MatchZyEventType.SidePicked,
            _                     => MatchZyEventType.Unknown,
        };
    }

    public class MatchEventRequest
    {
        [JsonPropertyName("event")]
        public string Event { get; set; }

        [JsonPropertyName("matchid")]
        public int MatchId { get; set; }

        [JsonPropertyName("map_number")]
        public int MapNumber { get; set; }

        [JsonPropertyName("winner")]
        public MatchEventWinner? Winner { get; set; }

        [JsonPropertyName("team1")]
        public MatchEventTeam? Team1 { get; set; }

        [JsonPropertyName("team2")]
        public MatchEventTeam? Team2 { get; set; }

        [JsonPropertyName("team1_series_score")]
        public int Team1SeriesScore { get; set; }

        [JsonPropertyName("team2_series_score")]
        public int Team2SeriesScore { get; set; }
    }

    public class MatchEventWinner
    {
        [JsonPropertyName("team")]
        public string? Team { get; set; }
    }

    public class MatchEventTeam
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("score_ct")]
        public int ScoreCt { get; set; }

        [JsonPropertyName("score_t")]
        public int ScoreT { get; set; }

        [JsonPropertyName("series_score")]
        public int SeriesScore { get; set; }

        [JsonPropertyName("players")]
        public List<MatchEventPlayer>? Players { get; set; }
    }

    public class MatchEventPlayer
    {
        [JsonPropertyName("steamid")]
        public string SteamId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("stats")]
        public MatchEventPlayerStats? Stats { get; set; }
    }

    public class MatchEventPlayerStats
    {
        [JsonPropertyName("kills")]
        public int Kills { get; set; }

        [JsonPropertyName("deaths")]
        public int Deaths { get; set; }

        [JsonPropertyName("assists")]
        public int Assists { get; set; }

        [JsonPropertyName("flash_assists")]
        public int FlashAssists { get; set; }

        [JsonPropertyName("team_kills")]
        public int TeamKills { get; set; }

        [JsonPropertyName("suicides")]
        public int Suicides { get; set; }

        [JsonPropertyName("damage")]
        public int Damage { get; set; }

        [JsonPropertyName("utility_damage")]
        public int UtilityDamage { get; set; }

        [JsonPropertyName("enemies_flashed")]
        public int EnemiesFlashed { get; set; }

        [JsonPropertyName("friendlies_flashed")]
        public int FriendliesFlashed { get; set; }

        [JsonPropertyName("knife_kills")]
        public int KnifeKills { get; set; }

        [JsonPropertyName("headshot_kills")]
        public int HeadshotKills { get; set; }

        [JsonPropertyName("rounds_played")]
        public int RoundsPlayed { get; set; }

        [JsonPropertyName("bomb_plants")]
        public int BombPlants { get; set; }

        [JsonPropertyName("bomb_defuses")]
        public int BombDefuses { get; set; }

        [JsonPropertyName("1k")]
        public int K1 { get; set; }

        [JsonPropertyName("2k")]
        public int K2 { get; set; }

        [JsonPropertyName("3k")]
        public int K3 { get; set; }

        [JsonPropertyName("4k")]
        public int K4 { get; set; }

        [JsonPropertyName("5k")]
        public int K5 { get; set; }

        [JsonPropertyName("1v1")]
        public int V1 { get; set; }

        [JsonPropertyName("1v2")]
        public int V2 { get; set; }

        [JsonPropertyName("1v3")]
        public int V3 { get; set; }

        [JsonPropertyName("1v4")]
        public int V4 { get; set; }

        [JsonPropertyName("1v5")]
        public int V5 { get; set; }

        [JsonPropertyName("first_kills_t")]
        public int FirstKillsT { get; set; }

        [JsonPropertyName("first_kills_ct")]
        public int FirstKillsCt { get; set; }

        [JsonPropertyName("first_deaths_t")]
        public int FirstDeathsT { get; set; }

        [JsonPropertyName("first_deaths_ct")]
        public int FirstDeathsCt { get; set; }

        [JsonPropertyName("trade_kills")]
        public int TradeKills { get; set; }

        [JsonPropertyName("kast")]
        public int Kast { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("mvp")]
        public int Mvp { get; set; }
    }
}
