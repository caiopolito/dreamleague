using dreamleague.domain.Aggregates.GetChampionshipById;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Championship;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Championships
{
    public class GetChampionshipByIdService : GenericService<GetChampionshipByIdRequest, GetChampionshipByIdResponse>, IGetChampionshipByIdService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetChampionshipByIdService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<GetChampionshipByIdResponse> OnExecute(GetChampionshipByIdRequest request)
        {
            return await unitOfWork.ChampionshipRepository.GetChampionshipByIdAsync(request);
        }
    }
}
