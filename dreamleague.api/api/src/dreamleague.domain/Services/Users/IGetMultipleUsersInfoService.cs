using dreamleague.domain.Aggregates.GetMultipleUsersInfo;
using dreamleague.domain.Aggregates.GetUserFriends;
using dreamleague.domain.Aggregates.GetUserInfo;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Users
{
    public interface IGetMultipleUsersInfoService : IService<GetMultipleUsersInfoRequest, GetMultipleUsersInfoResponse>
    {
    }
}
