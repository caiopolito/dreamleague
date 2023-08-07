using dreamleague.domain.Aggregates.GetUserInfo;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Users;
using dreamleague.shared.Services;
using FluentValidation;

namespace dreamleague.services.Services.Users
{
    public class GetUserInfoService : GenericService<GetUserInfoRequest, GetUserInfoResponse>, IGetUserInfoService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetUserInfoService(IUnitOfWork unitOfWork, IValidator<GetUserInfoRequest> validator) : base (validator)
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<GetUserInfoResponse> OnExecute(GetUserInfoRequest request)
        {
            var response = await unitOfWork.SteamUserRepository.GetPlayerSummariesAsync(new string[] { request.SteamId });
            var player = response.response.players.First();
            var playerInfo = await unitOfWork.PlayerRepository.GetPlayerInfoBySteamIdAsync(request.SteamId);

            if (playerInfo == null)
                playerInfo = await unitOfWork.PlayerRepository.InsertPlayerAsync(request.SteamId, player.personaname, player.avatarfull);

            if (playerInfo.Name != player.personaname || playerInfo.Avatar != player.avatarfull.ToString())
                await unitOfWork.PlayerRepository.UpdatePlayerNameAsync(request.SteamId, player.personaname, player.avatarfull);

            return new GetUserInfoResponse
            {
                ProfileUrl = player.profileurl,
                SteamId = player.steamid,
                Name = player.personaname,
                Coins = playerInfo.Coins,
                Rank = playerInfo.Rank,
                Points = playerInfo.Points,
                Avatar = player.avatarfull,
                IsAdmin = playerInfo.IsAdmin
            };
        }
    }
}
