using dreamleague.common.Entities.Steam;
using dreamleague.common.Entities.Steam.PlayerFriend;

namespace dreamleague.domain.Aggregates.GetUserFriends
{
    public class GetUserFriendsResponse
    {
        public List<Friend> friends { get; set; }

        public static implicit operator GetUserFriendsResponse(SteamResponses steamResponses)
        {
            return new GetUserFriendsResponse
            {
                friends = steamResponses.friendsList.friends
            };
        }
    }
}
