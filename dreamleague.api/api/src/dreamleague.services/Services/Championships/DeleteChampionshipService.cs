using dreamleague.domain.Aggregates.DeleteChampionship;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Championship;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Championships
{
    public class DeleteChampionshipService : GenericService<DeleteChampionshipRequest, DeleteChampionshipResponse>, IDeleteChampionshipService
    {
        private readonly IUnitOfWork unitOfWork;
        public DeleteChampionshipService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<DeleteChampionshipResponse> OnExecute(DeleteChampionshipRequest request)
        {
            await unitOfWork.ChampionshipRepository.DeleteChampionshipAsync(request);
            return new DeleteChampionshipResponse();
        }
    }
}
