namespace dreamleague.common.Entities.Championships
{
    public class Championship
    {
        public Guid Id { get; set; }
        public string  Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MinTeams { get; set; }
        public int PlayersOnTeam { get; set; }
    }
}
