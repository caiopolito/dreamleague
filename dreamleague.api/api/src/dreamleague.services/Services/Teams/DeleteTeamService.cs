using dreamleague.domain.Aggregates.DeleteTeam;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Team;
using dreamleague.shared.Services;
using System.Transactions;

namespace dreamleague.services.Services.Teams
{
    public class DeleteTeamService : GenericService<DeleteTeamRequest, DeleteTeamResponse>, IDeleteTeamService
    {
        private readonly IUnitOfWork unitOfWork;
        public DeleteTeamService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<DeleteTeamResponse> OnExecute(DeleteTeamRequest request)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                await unitOfWork.TeamRepository.DeleteAllTeamPlayersAsync(request.TeamId);

                await unitOfWork.NotificationRepository.DeleteNotificationsByTeamIdAsync(request.TeamId);

                await unitOfWork.TeamRepository.DeleteTeamAsync(request);

                transactionScope.Complete();
                return new DeleteTeamResponse();
            }
            catch (Exception)
            {
                transactionScope.Dispose();
                throw;
            }
        }
    }
}
