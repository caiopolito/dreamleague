using dreamleague.domain.Aggregates.UpdateChampionship;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Championship;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Championships
{
    public class UpdateChampionshipService : GenericService<UpdateChampionshipRequest, UpdateChampionshipResponse>, IUpdateChampionshipService
    {
        private readonly IUnitOfWork unitOfWork;
        public UpdateChampionshipService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<UpdateChampionshipResponse> OnExecute(UpdateChampionshipRequest request)
        {
            return await unitOfWork.ChampionshipRepository.UpdateChampionshipAsync(request);
        }
    }
}
