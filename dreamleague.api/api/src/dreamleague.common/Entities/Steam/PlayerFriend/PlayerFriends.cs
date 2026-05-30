namespace dreamleague.common.Entities.Steam.PlayerFriend
{
    public class PlayerFriends
    {
        public PlayerFriends()
        {
            friends = new();
        }
        public List<Friend> friends { get; set; }
    }
}
