using System.Text.Json.Serialization;

namespace dreamleague.domain.Aggregates.MatchEvent
{
    public class MatchEventRequest
    {
        [JsonPropertyName("event")]
        public string Event { get; set; }

        [JsonPropertyName("matchid")]
        public int MatchId { get; set; }

        [JsonPropertyName("winner")]
        public MatchEventWinner? Winner { get; set; }
    }

    public class MatchEventWinner
    {
        [JsonPropertyName("team")]
        public string Team { get; set; }
    }
}
