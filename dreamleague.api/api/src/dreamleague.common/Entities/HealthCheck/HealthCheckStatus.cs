namespace dreamleague.common.Entities.HealthCheck
{
    public record HealthCheckStatus(string Version, IEnumerable<HealthCheckActivity> Activities)
    {
        public bool Success => Activities.All(activity => activity.Success);
    };
}
