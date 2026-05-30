namespace dreamleague.common.Entities.Players
{
    public class PlayerFilter
    {
        public string? Name { get; set; }
        public int? Points { get; set; }
        public IEnumerable<string> NotIn { get; set; }
    }
}
