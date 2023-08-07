using dreamleague.domain.Aggregates.StartMatchMap;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Match;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Matches
{
    public class StartMatchMapService : GenericService<StartMatchMapRequest, StartMatchMapResponse>,IStartMatchMapService
    {
        private readonly IUnitOfWork unitOfWork;
        public StartMatchMapService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<StartMatchMapResponse> OnExecute(StartMatchMapRequest request)
        {
            var match = await unitOfWork.MatchRepository.GetMatchByIdAsync(request.MatchId);

            match.StartTime = DateTime.UtcNow;

            await unitOfWork.MatchRepository.UpdateMatchAsync(match);

            await unitOfWork.MatchRepository.CreateMatchMapStatsAsync(request.MatchId, request.MapNumber, request.MapName);

            return new StartMatchMapResponse();
        }
    }
}
