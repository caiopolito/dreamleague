using dreamleague.domain.Aggregates.GetUsers;

namespace dreamleague.domain.Entities.Players
{
    public class PlayerFilter
    {
        public string? Name { get; set; }
        public int? Points { get; set; }
        public IEnumerable<string> NotIn { get; set; }

        public static implicit operator PlayerFilter(GetUsersRequest entity)
        {
            return new PlayerFilter
            {
                Name = entity?.Name,
                Points = (int)entity?.Rank * 100, 
                NotIn = entity?.NotIn
            };
        }
    }
}
