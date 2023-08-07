using dreamleague.domain.Entities.Championships;

namespace dreamleague.domain.Aggregates.UpdateChampionship
{
    public class UpdateChampionshipResponse 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MinTeams { get; set; }
        public int PlayersOnTeam { get; set; }

        public static implicit operator UpdateChampionshipResponse(Championship entity)
        {
            return new UpdateChampionshipResponse
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
