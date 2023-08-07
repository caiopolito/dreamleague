using dreamleague.domain.Aggregates.GetUserInfo;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Users
{
    public interface IGetUserInfoService : IService<GetUserInfoRequest, GetUserInfoResponse>
    {
    }
}
