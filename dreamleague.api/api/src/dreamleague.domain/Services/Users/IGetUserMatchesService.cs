using dreamleague.domain.Aggregates.GetUserMatches;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Users
{
    public interface IGetUserMatchesService : IService<GetUserMatchesRequest, GetUserMatchesResponse>
    {
    }
}
