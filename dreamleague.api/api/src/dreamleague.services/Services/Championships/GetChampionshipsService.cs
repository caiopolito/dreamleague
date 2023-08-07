using dreamleague.domain.Aggregates.GetChampionships;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Championship;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Championships
{
    public class GetChampionshipsService : GenericService<GetChampionshipsRequest, GetChampionshipsResponse>, IGetChampionshipsService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetChampionshipsService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<GetChampionshipsResponse> OnExecute(GetChampionshipsRequest request)
        {
            return new GetChampionshipsResponse
            {
                Championships = await unitOfWork.ChampionshipRepository.GetChampionshipsAsync(request)
            };
        }
    }
}
