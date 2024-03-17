using dreamleague.domain.Aggregates.GetUsers;
using dreamleague.common.Entities.Players;

namespace dreamleague.domain.Adapters.GetUsers
{
    public interface IGetUsersAdapter
    {
        PlayerFilter ToPlayerFilter(GetUsersRequest request);
    }
}
