namespace dreamleague.domain.Infrastructure
{
    public interface IDatabaseRepository
    {
        Task<bool> GetHealthCheckAsync();
    }
}
