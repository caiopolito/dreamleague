namespace dreamleague.common.Entities.Get5
{
    public class MapStats
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int MapNumber { get; set; }
        public string MapName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid? Winner { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
    }
}
