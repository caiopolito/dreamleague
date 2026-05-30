using dreamleague.common.Enums.Ranks;

namespace dreamleague.common.Entities.Players
{
    public class Player
    {
        public string? SteamId { get; set; }
        public string? Name { get; set; }
        public int Coins { get; set; }
        public int Points { get; set; }
        public bool IsAdmin { get; set; }
        public string Avatar { get; set; }
        public RanksEnum Rank { get => (RanksEnum)(Points / 100); }
    }
}
