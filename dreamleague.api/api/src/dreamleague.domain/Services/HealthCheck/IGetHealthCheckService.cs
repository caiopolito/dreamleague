using dreamleague.common.Entities.HealthCheck;
using dreamleague.shared.Services;

namespace dreamleague.domain.Services.HealthCheck
{
    public interface IGetHealthCheckService : IServiceOnlyResponse<HealthCheckStatus>
    {
    }
}
