using dreamleague.domain.Aggregates.UpdateMatchMap;
using dreamleague.domain.Entities.Get5;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Match;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Matches
{
    public class UpdateMatchMapService : GenericService<UpdateMatchMapRequest, UpdateMatchMapResponse>, IUpdateMatchMapService
    {
        private readonly IUnitOfWork unitOfWork;
        public UpdateMatchMapService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<UpdateMatchMapResponse> OnExecute(UpdateMatchMapRequest request)
        {
            await unitOfWork.MatchRepository.UpdateMatchMapStatsAsync(new MapStats
            {
                MatchId = request.MatchId,
                MapNumber = request.MapNumber,
                Team1Score = request.Team1Score,
                Team2Score = request.Team2Score                
            });

            return new UpdateMatchMapResponse();
        }
    }
}
