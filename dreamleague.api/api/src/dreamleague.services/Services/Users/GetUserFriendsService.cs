using dreamleague.domain.Aggregates.GetUserFriends;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Users;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Users
{
    public class GetUserFriendsService : GenericService<GetUserFriendsRequest, GetUserFriendsResponse>, IGetUserFriendsService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetUserFriendsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task<GetUserFriendsResponse> OnExecute(GetUserFriendsRequest request)
        {
            var steamFriends = await unitOfWork.SteamUserRepository.GetPlayerFriendsAsync(request.SteamId);
            var dreamLeaguePlayers = await unitOfWork.PlayerRepository.GetAllPlayersAsync();
            return new GetUserFriendsResponse
            {
                friends = steamFriends.friendsList.friends.Where(x =>
                    dreamLeaguePlayers.Any(y => y.SteamId == x.steamid)
                ).ToList()
            };
        }
    }
}
