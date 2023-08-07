namespace dreamleague.domain.Aggregates.FinishMatch
{
    public class FinishMatchRequest
    {
        // Route parameters
        public int MatchId { get; set; }

        // Form parameters
        public string Winner { get; set; }
        public int Forfeit { get; set; }
    }
}
