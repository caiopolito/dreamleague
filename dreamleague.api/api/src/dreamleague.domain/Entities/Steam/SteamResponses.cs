using dreamleague.domain.Entities.Steam.PlayerSummary;
using dreamleague.domain.Entities.Steam.PlayerFriend;

namespace dreamleague.domain.Entities.Steam
{
    public class SteamResponses
    {
        public SteamResponses()
        {

        }
        public PlayersSummaries? response { get; set; }
        public PlayerFriends friendsList { get; set; }
    }
}
