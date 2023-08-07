using dreamleague.domain.Aggregates.GetUserMatches;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Users;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Users
{
    public class GetUserMatchesService : GenericService<GetUserMatchesRequest, GetUserMatchesResponse>, IGetUserMatchesService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetUserMatchesService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<GetUserMatchesResponse> OnExecute(GetUserMatchesRequest request)
        {
            return new GetUserMatchesResponse
            {
                Matches = await unitOfWork.MatchRepository.GetPlayerMatchesBySteamIdAsync(request.SteamId)
            };
        }
    }
}
