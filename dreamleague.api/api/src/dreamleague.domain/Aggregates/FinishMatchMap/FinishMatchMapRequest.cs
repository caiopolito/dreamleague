namespace dreamleague.domain.Aggregates.FinishMatchMap
{
    public class FinishMatchMapRequest
    {
        public int MatchId { get; set; }
        public int MapNumber { get; set; }
        public string Winner { get; set; }
    }
}
