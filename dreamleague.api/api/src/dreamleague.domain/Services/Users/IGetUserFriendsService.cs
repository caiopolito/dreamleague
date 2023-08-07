using dreamleague.domain.Aggregates.GetUserFriends;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Users
{
    public interface IGetUserFriendsService : IService<GetUserFriendsRequest, GetUserFriendsResponse>
    {
    }
}
