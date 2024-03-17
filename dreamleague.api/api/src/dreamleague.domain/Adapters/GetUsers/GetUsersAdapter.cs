using dreamleague.domain.Aggregates.GetUsers;
using dreamleague.common.Entities.Players;
using SharpCompress.Common;

namespace dreamleague.domain.Adapters.GetUsers
{
    public class GetUsersAdapter : IGetUsersAdapter
    {
        public PlayerFilter ToPlayerFilter(GetUsersRequest request)
        {
            return new PlayerFilter
            {
                Name = request?.Name,
                Points = (int)request?.Rank * 100,
                NotIn = request?.NotIn
            };
        }
    }
}
