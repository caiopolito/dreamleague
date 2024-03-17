using dreamleague.domain.Adapters.CreateMatch;
using dreamleague.domain.Adapters.GetUsers;

namespace dreamleague.api.Configurations
{
    public static class AdaptersConfig
    {
        public static void ConfigAdapters(this IServiceCollection services) =>
            services.AddScoped<ICreateMatchAdapter, CreateMatchAdapter>()
            .AddScoped<IGetUsersAdapter, GetUsersAdapter>();
    }
}
