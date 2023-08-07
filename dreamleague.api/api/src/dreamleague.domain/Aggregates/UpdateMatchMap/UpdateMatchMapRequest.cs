namespace dreamleague.domain.Aggregates.UpdateMatchMap
{
    public class UpdateMatchMapRequest
    {
        public int MatchId { get; set; }
        public int MapNumber { get; set; }
        public int Key { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
    }
}
