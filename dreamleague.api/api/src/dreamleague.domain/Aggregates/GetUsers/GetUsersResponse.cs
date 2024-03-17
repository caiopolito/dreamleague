using dreamleague.common.Entities.Players;

namespace dreamleague.domain.Aggregates.GetUsers
{
    public class GetUsersResponse
    {
        public IEnumerable<Player> Players { get; set; }
    }
}
