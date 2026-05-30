namespace dreamleague.common.Entities.Get5
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
    }
}
