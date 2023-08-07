using dreamleague.domain.Aggregates.GetMatchDetails;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Match
{
    public interface IGetMatchDetailsService : IService<GetMatchDetailsRequest, GetMatchDetailsResponse>
    {
    }
}
