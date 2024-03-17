using dreamleague.domain.Aggregates.GetUsers;
using dreamleague.domain.Adapters.GetUsers;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Users;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Users
{
    public class GetUsersService : GenericService<GetUsersRequest, GetUsersResponse>, IGetUsersService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGetUsersAdapter adapter;
        public GetUsersService
            (
                IUnitOfWork unitOfWork,
                IGetUsersAdapter adapter
            )
        {
            this.adapter = adapter;
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<GetUsersResponse> OnExecute(GetUsersRequest request)
        {
            if (request.TeamId is not null)
            {
                var teamPlayers = await unitOfWork.TeamRepository.GetTeamPlayersAsync(request.TeamId.Value);
                request.NotIn = teamPlayers.Select(x => { return x.SteamId; });
            }

            var players = await unitOfWork.PlayerRepository.GetAllPlayersAsync(adapter.ToPlayerFilter(request));

            if (request.SteamId is not null)
                players = players.Where(x => x.SteamId != request.SteamId).ToList();

            return new GetUsersResponse
            {
                Players = players
            };
        }
    }
}
