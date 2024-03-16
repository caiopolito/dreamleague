using dreamleague.domain.Aggregates.CreateMatch;

namespace dreamleague.domain.Entities.Get5
{
    public class Match
    {
        public string MatchId { get; set; }
        public string MatchTitle { get => "match_live"; }
        public Guid ServerId { get; set; }
        public Guid Team1Id { get; set; }
        public Guid Team2Id { get; set; }
        public string Team1String { get; set; }
        public string Team2String { get; set; }
        public Guid? Winner { get; set; }
        public string PluginVersion { get; set; }
        public bool? Forfeit { get; set; }
        public bool? Cancelled { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int MaxMaps { get; set; }
        public bool SkipVeto { get; set; }
        public string ApiKey { get; set; }
        public int? Team1Score { get; set; }
        public int? Team2Score { get; set; }

        public static implicit operator Match(CreateMatchRequest entity)
        {
            return new Match { 
                Team1Id = entity.Team1Id.Value,
                Team2Id = entity.Team2Id.Value,
                Team1String = entity.Team1String,
                Team2String = entity.Team2String,
                ApiKey = entity.ApiKey.ToString()
            };
        }
    }
}
