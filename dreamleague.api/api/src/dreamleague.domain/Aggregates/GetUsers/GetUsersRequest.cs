using dreamleague.domain.Enums.Ranks;

namespace dreamleague.domain.Aggregates.GetUsers
{
    public class GetUsersRequest
    {
        public string? Name { get; set; }
        public RanksEnum? Rank { get; set; }
        public bool? IsFriend { get; set; }
        public string? SteamId { get; set; }
        public Guid? TeamId { get; set; }
        public IEnumerable<string>? NotIn { get; set; }
    }
}
