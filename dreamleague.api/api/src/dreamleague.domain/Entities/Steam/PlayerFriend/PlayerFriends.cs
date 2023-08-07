namespace dreamleague.domain.Entities.Steam.PlayerFriend
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
