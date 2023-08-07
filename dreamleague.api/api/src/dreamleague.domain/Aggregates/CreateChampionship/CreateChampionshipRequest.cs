namespace dreamleague.domain.Aggregates.CreateChampionship
{
    public class CreateChampionshipRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int MinTeams { get; set; }
        public int PlayersOnTeam { get; set; }
    }
}
