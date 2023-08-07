using dreamleague.domain.Aggregates.GetMatchDetails;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Match;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Matches
{
    public class GetMatchDetailsService : GenericService<GetMatchDetailsRequest, GetMatchDetailsResponse>, IGetMatchDetailsService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetMatchDetailsService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<GetMatchDetailsResponse> OnExecute(GetMatchDetailsRequest request)
        {
            var match = await unitOfWork.MatchRepository.GetMatchByIdAsync(request.MatchId);

            return new GetMatchDetailsResponse
            {
                TeamOne = match.Team1String,
                TeamTwo = match.Team2String,
                PlayersTeamOne = await unitOfWork.MatchRepository.GetPlayersByMatchAndTeamIdAsync(request.MatchId, match.Team1Id),
                PlayersTeamTwo = await unitOfWork.MatchRepository.GetPlayersByMatchAndTeamIdAsync(request.MatchId, match.Team2Id),
        };
        }
    }
}
