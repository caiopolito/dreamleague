using dreamleague.domain.Enums.Ranks;

namespace dreamleague.domain.Aggregates.GetUserInfo
{
    public class GetUserInfoResponse
    {
        public Uri? ProfileUrl { get; set; }
        public string? Name { get; set; }
        public string? SteamId { get; set; }
        public RanksEnum Rank { get; set; }
        public RanksEnum NextRank { get => (RanksEnum)(Points / 100)+1; }
        public int Points { get; set; }
        public int Coins { get; set; }
        public Uri? Avatar { get; set; }
        public bool IsAdmin { get; set; }
    }
}
