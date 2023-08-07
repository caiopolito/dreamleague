namespace dreamleague.domain.Entities.Get5
{
    public class MatchResult
    {
        public string MatchId { get; set; }
        public string MatchTitle { get; set; }
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }
        public string Team1String { get; set; }
        public string Team2String { get; set; }
        public int? Winner { get; set; }
        public bool? Forfeit { get; set; }
        public bool? Cancelled { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Team1Score { get; set; }
        public int? Team2Score { get; set; }
        public string? MapName { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
    }
}
