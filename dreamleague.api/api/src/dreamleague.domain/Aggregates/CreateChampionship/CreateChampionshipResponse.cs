using dreamleague.common.Entities.Championships;

namespace dreamleague.domain.Aggregates.CreateChampionship
{
    public class CreateChampionshipResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MinTeams { get; set; }
        public int PlayersOnTeam { get; set; }

        public static implicit operator CreateChampionshipResponse(Championship entity)
        {
            return new CreateChampionshipResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                MinTeams = entity.MinTeams,
                PlayersOnTeam = entity.PlayersOnTeam
            };
        }
    }
}
