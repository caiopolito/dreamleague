using dreamleague.domain.Adapters.CreateMatch;

namespace dreamleague.api.Configurations
{
    public static class AdaptersConfig
    {
        public static void ConfigAdapters(this IServiceCollection services) =>
            services.AddScoped<ICreateMatchAdapter, CreateMatchAdapter>();
    }
}
