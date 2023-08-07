using dreamleague.domain.Aggregates.CreateTeam;
using dreamleague.domain.Aggregates.DeleteTeam;
using dreamleague.domain.Aggregates.GetTeamById;
using dreamleague.domain.Aggregates.GetTeams;
using dreamleague.domain.Aggregates.UpdateTeam;
using dreamleague.domain.Entities.Teams;

namespace dreamleague.domain.Infrastructure
{
    public interface ITeamRepository : IDatabaseRepository
    {
        Task<Team> CreateTeamAsync(CreateTeamRequest request);
        Task<Team> UpdateTeamAsync(UpdateTeamRequest request);
        Task DeleteTeamAsync(DeleteTeamRequest request);
        Task<Team> GetTeamByIdAsync(GetTeamByIdRequest request);
        Task InsertPlayerIntoTeamAsync(PlayerTeam player, Guid teamId);
        Task InsertPlayersIntoTeamAsync(IEnumerable<PlayerTeam> player, Guid teamId);
        Task<IEnumerable<PlayerTeam>> GetTeamPlayersAsync(Guid teamId);
        Task<IEnumerable<Team>> GetTeamsAsync(GetTeamsRequest request);
        Task DeleteAllTeamPlayersAsync(Guid teamId);
        Task RemovePlayerAsync(string steamId, Guid teamId);

    }
}
