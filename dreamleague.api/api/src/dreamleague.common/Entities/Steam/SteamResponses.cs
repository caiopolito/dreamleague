using dreamleague.common.Entities.Steam.PlayerSummary;
using dreamleague.common.Entities.Steam.PlayerFriend;

namespace dreamleague.common.Entities.Steam
{
    public class SteamResponses
    {
        public PlayersSummaries? response { get; set; }
        public PlayerFriends friendsList { get; set; }
    }
}
