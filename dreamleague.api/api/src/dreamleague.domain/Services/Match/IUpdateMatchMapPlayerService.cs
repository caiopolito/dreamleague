using dreamleague.domain.Aggregates.UpdateMatchMapPlayer;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Match
{
    public interface IUpdateMatchMapPlayerService : IService<UpdateMatchMapPlayerRequest, UpdateMatchMapPlayerResponse>
    {
    }
}
