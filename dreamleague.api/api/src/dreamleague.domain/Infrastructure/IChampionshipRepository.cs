using CoreRCON.Parsers.Csgo;
using dreamleague.domain.Aggregates.CreateChampionship;
using dreamleague.domain.Aggregates.DeleteChampionship;
using dreamleague.domain.Aggregates.GetChampionshipById;
using dreamleague.domain.Aggregates.GetChampionships;
using dreamleague.domain.Aggregates.UpdateChampionship;
using dreamleague.common.Entities.Championships;
using dreamleague.common.Entities.Teams;

namespace dreamleague.domain.Infrastructure
{
    public interface IChampionshipRepository : IDatabaseRepository
    {
        Task<Championship> CreateChampionshipAsync(CreateChampionshipRequest request);
        Task<Championship> UpdateChampionshipAsync(UpdateChampionshipRequest request);
        Task DeleteChampionshipAsync(DeleteChampionshipRequest request);
        Task<IEnumerable<Championship>> GetChampionshipsAsync(GetChampionshipsRequest request);
        Task<Championship> GetChampionshipByIdAsync(GetChampionshipByIdRequest request);
        Task<IEnumerable<Team>> GetChampionshipTeamsAsync(Guid championshipId);
        Task RegisterTeamAsync(Guid teamId, Guid championshipId);
        Task RemoveTeamAsync(Guid teamId, Guid championshipId);
    }
}
