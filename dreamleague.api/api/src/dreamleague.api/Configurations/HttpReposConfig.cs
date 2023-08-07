using dreamleague.domain.Infrastructure;
using dreamleague.infrastructure.Repositories.SteamUsers;
using dreamleague.shared.Configurations;

namespace dreamleague.api.Configurations
{
    public static class HttpReposConfig
    {
        public static void ConfigHttpRepos(this IServiceCollection services, ApplicationConfig applicationConfig) =>
            services.AddHttpClient<ISteamUserRepository, SteamUserRepository>(client =>
            {
                client.BaseAddress = applicationConfig.Apis.SteamApi;
            });
    }
}
