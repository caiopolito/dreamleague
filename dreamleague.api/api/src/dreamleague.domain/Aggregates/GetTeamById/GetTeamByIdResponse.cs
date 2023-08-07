using dreamleague.domain.Entities.Teams;

namespace dreamleague.domain.Aggregates.GetTeamById
{
    public class GetTeamByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PlayerTeam> Players { get; set; }

        public static implicit operator GetTeamByIdResponse(Team entity)
        {
            return new GetTeamByIdResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Players = entity.Players
            };
        }
    }
}
