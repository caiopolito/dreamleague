using dreamleague.domain.Services.Championship;
using dreamleague.domain.Services.Chat;
using dreamleague.domain.Services.HealthCheck;
using dreamleague.domain.Services.Match;
using dreamleague.domain.Services.Team;
using dreamleague.domain.Services.Users;
using dreamleague.services.Services.Championships;
using dreamleague.services.Services.Chat;
using dreamleague.services.Services.HealthCheck;
using dreamleague.services.Services.Matches;
using dreamleague.services.Services.Teams;
using dreamleague.services.Services.Users;

namespace dreamleague.api.Configurations
{
    public static class ServicesConfig
    {
        public static void ConfigServices(this IServiceCollection services) => services
            // Chats
            .AddScoped<ISendMessageService, SendMessageService>()
            .AddScoped<IGetOrCreateChatInfoService, GetOrCreateChatInfoService>()

            // Users
            .AddScoped<IGetUserFriendsService, GetUserFriendsService>()
            .AddScoped<IGetMultipleUsersInfoService, GetMultipleUsersInfoService>()
            .AddScoped<IGetUserInfoService, GetUserInfoService>()
            .AddScoped<IGetUserMatchesService, GetUserMatchesService>()
            .AddScoped<IGetUsersService, GetUsersService>()
            .AddScoped<IGetUserNotificationsService, GetUserNotificationsService>()
            .AddScoped<IRefuseUserNotificationService, RefuseUserNotificationService>()
            .AddScoped<IAcceptUserNotificationService, AcceptUserNotificationService>()
            .AddScoped<ISetUserNotificationsAsSeenService, SetUserNotificationsAsSeenService>()
            .AddScoped<ICheckIfHasTeamService, CheckIfHasTeamService>()

            // Matches
            .AddScoped<ICreateMatchService, CreateMatchService>()
            .AddScoped<IMatchEventService, MatchEventService>()
            .AddScoped<IFinishMatchService, FinishMatchService>()
            .AddScoped<IGetMatchDetailsService, GetMatchDetailsService>()

            // Teams
            .AddScoped<ICreateTeamService, CreateTeamService>()
            .AddScoped<IUpdateTeamService, UpdateTeamService>()
            .AddScoped<IGetTeamByIdService, GetTeamByIdService>()
            .AddScoped<IDeleteTeamService, DeleteTeamService>()
            .AddScoped<IGetTeamsService, GetTeamsService>()
            .AddScoped<IInvitePlayersToTeamService, InvitePlayersToTeamService>()
            .AddScoped<IRemovePlayerFromTeamService, RemovePlayerFromTeamService>()

            // Championships
            .AddScoped<ICreateChampionshipService, CreateChampionshipService>()
            .AddScoped<IGetChampionshipsService, GetChampionshipsService>()
            .AddScoped<IUpdateChampionshipService, UpdateChampionshipService>()
            .AddScoped<IDeleteChampionshipService, DeleteChampionshipService>()
            .AddScoped<IGetChampionshipByIdService, GetChampionshipByIdService>()
            .AddScoped<IRegisterTeamToChampionshipService, RegisterTeamToChampionshipService>()
            .AddScoped<IGetChampionshipTeamsService, GetChampionshipTeamsService>()
            .AddScoped<IRemoveTeamFromChampionshipService, RemoveTeamFromChampionshipService>()

            // Healthcheck
            .AddScoped<IGetHealthCheckService, GetHealthCheckService>();
    }
}
