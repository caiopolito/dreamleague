using dreamleague.domain.Aggregates.RegisterTeamToChampionship;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Championship
{
    public interface IRegisterTeamToChampionshipService : IService<RegisterTeamToChampionshipRequest, RegisterTeamToChampionshipResponse>
    {
    }
}
