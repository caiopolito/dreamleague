using dreamleague.domain.Infrastructure;
using dreamleague.infrastructure.Database;
using dreamleague.infrastructure.Repositories.Azure;
using dreamleague.infrastructure.Repositories.Championships;
using dreamleague.infrastructure.Repositories.Chats;
using dreamleague.infrastructure.Repositories.Matches;
using dreamleague.infrastructure.Repositories.Notifications;
using dreamleague.infrastructure.Repositories.Players;
using dreamleague.infrastructure.Repositories.Rcon;
using dreamleague.infrastructure.Repositories.Servers;
using dreamleague.infrastructure.Repositories.SteamUsers;
using dreamleague.infrastructure.Repositories.Teams;
using dreamleague.infrastructure.UoW;

namespace dreamleague.api.Configurations
{
    public static class InfrastructuresConfig
    {
        public static void ConfigInfrastructures(this IServiceCollection services) =>
            services.AddScoped<ISteamUserRepository, SteamUserRepository>()
                    .AddScoped<IPlayerRepository, PlayerRepository>()
                    .AddScoped<IChatRepository, ChatRepository>()
                    .AddScoped<IServerRepository, ServerRepository>()
                    .AddScoped<IMatchRepository, MatchRepository>()
                    .AddScoped<IChampionshipRepository, ChampionshipRepository>()
                    .AddScoped<ITeamRepository, TeamRepository>()
                    .AddScoped<IAzureBlobStorageRepository, AzureBlobStorageRepository>()
                    .AddScoped<INotificationRepository, NotificationRepository>()
                    .AddScoped<IRconRepository, RconRepository>()
                    .AddScoped<IUnitOfWork, UnitOfWork>()
                    .AddScoped<IDatabaseFactory, DatabaseFactory>();
    }
}
