using Azure;
using dreamleague.domain.Aggregates.GetMultipleUsersInfo;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Users;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Users
{
    public class GetMultipleUsersInfoService : GenericService<GetMultipleUsersInfoRequest, GetMultipleUsersInfoResponse>, IGetMultipleUsersInfoService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetMultipleUsersInfoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        protected override async Task<GetMultipleUsersInfoResponse> OnExecute(GetMultipleUsersInfoRequest request)
        {
            var response = await unitOfWork.SteamUserRepository.GetPlayerSummariesAsync(request.SteamIds);


            return response.Content;
        }
    }
}
