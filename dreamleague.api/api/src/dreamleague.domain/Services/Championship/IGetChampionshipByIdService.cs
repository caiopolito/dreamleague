using dreamleague.domain.Aggregates.GetChampionshipById;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.Championship
{
    public interface IGetChampionshipByIdService : IService<GetChampionshipByIdRequest, GetChampionshipByIdResponse>
    {
    }
}
