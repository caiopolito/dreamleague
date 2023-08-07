using dreamleague.domain.Aggregates.CreateChampionship;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Championship;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Championships
{
    public class CreateChampionshipService : GenericService<CreateChampionshipRequest, CreateChampionshipResponse>, ICreateChampionshipService
    {
        private readonly IUnitOfWork unitOfWork;
        public CreateChampionshipService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<CreateChampionshipResponse> OnExecute(CreateChampionshipRequest request)
        {
            return await unitOfWork.ChampionshipRepository.CreateChampionshipAsync(request);
        }
    }
}
