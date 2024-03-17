using dreamleague.common.Entities.Teams;

namespace dreamleague.domain.Aggregates.UpdateTeam
{
    public class UpdateTeamResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static implicit operator UpdateTeamResponse(Team entity)
        {
            return new UpdateTeamResponse
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
