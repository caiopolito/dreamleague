namespace dreamleague.shared.Services
{
    public interface IService<TRequest, TResponse>
    {
        Task<TResponse> Execute(TRequest request);
    }
}
