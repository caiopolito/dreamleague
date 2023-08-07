using dreamleague.domain.Entities.HealthCheck;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.HealthCheck;

namespace dreamleague.services.Services.HealthCheck
{
    public class GetHealthCheckService : IGetHealthCheckService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetHealthCheckService(
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<HealthCheckStatus> Execute()
        {
            var activities = new HealthCheckActivity[]
            {
                new(Name: "Players", Success: await unitOfWork.PlayerRepository.GetHealthCheckAsync()),
                new(Name: "Chats", Success: await unitOfWork.ChatRepository.GetHealthCheckAsync()),
                new(Name: "Servers", Success: await unitOfWork.ServerRepository.GetHealthCheckAsync()),
                new(Name: "Matches", Success: await unitOfWork.MatchRepository.GetHealthCheckAsync()),
                new(Name: "Teams", Success: await unitOfWork.TeamRepository.GetHealthCheckAsync()),
                new(Name: "Championships", Success: await unitOfWork.ChampionshipRepository.GetHealthCheckAsync()),
                new(Name: "Notifications", Success: await unitOfWork.NotificationRepository.GetHealthCheckAsync())
            };

            return new HealthCheckStatus("1.0.0.0", activities);
        }
    }
}
