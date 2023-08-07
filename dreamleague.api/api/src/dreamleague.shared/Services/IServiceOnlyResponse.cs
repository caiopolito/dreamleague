namespace dreamleague.shared.Services
{
    public interface IServiceOnlyResponse<TResponse>
    {
        Task<TResponse> Execute();
    }
}
