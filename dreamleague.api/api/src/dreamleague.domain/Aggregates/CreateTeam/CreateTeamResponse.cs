using dreamleague.domain.Entities.Teams;

namespace dreamleague.domain.Aggregates.CreateTeam
{
    public class CreateTeamResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static implicit operator CreateTeamResponse(Team entity)
        {
            return new CreateTeamResponse
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
