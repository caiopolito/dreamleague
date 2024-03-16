namespace dreamleague.domain.Entities.Get5
{
    public class TeamMatch
    {
        public Guid id { get; set; }
        public string name { get; set; } = "";
        public string tag { get; set; }
        public string flag { get; set; } = "BR";
        public string logo { get; set; } = "";
        public Dictionary<string, string> players { get; set; }
    }
}
