using dreamleague.domain.Aggregates.GetTeamById;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Team
{
    public interface IGetTeamByIdService : IService<GetTeamByIdRequest, GetTeamByIdResponse>
    {
    }
}
