using dreamleague.domain.Aggregates.CreateMatch;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Match
{
    public interface ICreateMatchService : IService<CreateMatchRequest, CreateMatchResponse>
    {
    }
}
