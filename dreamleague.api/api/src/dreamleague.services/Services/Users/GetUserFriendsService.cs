using dreamleague.domain.Aggregates.GetUserFriends;
using dreamleague.common.Entities.Steam;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Users;
using dreamleague.shared.Services;
using System.Net;

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


            // Usually indicates that the user has a private friends list
            // TODO: Identify whether it's just a private profile or a real unauthorized request
            if (steamFriends.StatusCode == HttpStatusCode.Unauthorized)
            {
                steamFriends.Content = new SteamResponses();
            }

            return new GetUserFriendsResponse
            {
                friends = steamFriends.Content.friendsList.friends.Where(x =>
                    dreamLeaguePlayers.Any(y => y.SteamId == x.steamid)
                ).ToList()
            };
        }
    }
}
