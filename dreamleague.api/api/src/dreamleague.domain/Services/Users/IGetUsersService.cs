using dreamleague.domain.Aggregates.GetUsers;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Users
{
    public interface IGetUsersService : IService<GetUsersRequest, GetUsersResponse>
    {
    }
}
