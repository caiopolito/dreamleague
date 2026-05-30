using dreamleague.domain.Aggregates.FinishMatch;
using dreamleague.domain.Aggregates.MatchEvent;
using dreamleague.domain.Services.Match;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Matches
{
    public class MatchEventService : GenericService<MatchEventRequest, object>, IMatchEventService
    {
        private readonly IFinishMatchService finishMatchService;

        public MatchEventService(IFinishMatchService finishMatchService)
        {
            this.finishMatchService = finishMatchService;
        }

        protected override async Task<object> OnExecute(MatchEventRequest request)
        {
            if (request.Event == "series_end")
            {
                string winner = request.Winner?.Team ?? "none";
                bool cancelled = winner == "none" || winner == null;

                await finishMatchService.Execute(new FinishMatchRequest
                {
                    MatchId = request.MatchId,
                    Winner = winner,
                    Forfeit = cancelled ? 1 : 0,
                });
            }

            return new { };
        }
    }
}
