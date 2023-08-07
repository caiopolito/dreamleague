using dreamleague.domain.Aggregates.AcceptUserNotification;
using dreamleague.domain.Aggregates.GetUserNotifications;
using dreamleague.domain.Aggregates.RefuseUserNotification;
using dreamleague.domain.Aggregates.SetUserNotificationsAsSeen;
using dreamleague.domain.Services.Users;
using Microsoft.AspNetCore.SignalR;

namespace dreamleague.hubs.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IGetUserNotificationsService getUserNotificationsService;
        private readonly ISetUserNotificationsAsSeenService setUserNotificationsAsSeenService;
        private readonly IAcceptUserNotificationService acceptUserNotificationService;
        private readonly IRefuseUserNotificationService refuseUserNotificationService;
        public NotificationHub
            (
                IGetUserNotificationsService getUserNotificationsService,
                ISetUserNotificationsAsSeenService setUserNotificationsAsSeenService,
                IAcceptUserNotificationService acceptUserNotificationService,
                IRefuseUserNotificationService refuseUserNotificationService
            )
        {
            this.getUserNotificationsService = getUserNotificationsService;
            this.setUserNotificationsAsSeenService = setUserNotificationsAsSeenService;
            this.acceptUserNotificationService = acceptUserNotificationService;
            this.refuseUserNotificationService = refuseUserNotificationService;
        }
        public async Task GetNotificationsAsync(string steamId)
        {
            var notifications = await getUserNotificationsService.Execute(new GetUserNotificationsRequest { SteamId = steamId });

            await Clients.Caller.SendAsync("ReceiveNotifications", notifications);
        }

        public async Task SetNotificationsAsSeenAsync(string steamId)
        {
            await setUserNotificationsAsSeenService.Execute(new SetUserNotificationsAsSeenRequest { SteamId = steamId });

            var notifications = await getUserNotificationsService.Execute(new GetUserNotificationsRequest { SteamId = steamId });

            await Clients.Caller.SendAsync("ReceiveNotifications", notifications);
        }

        public async Task AcceptNotificationAsync(string steamId, Guid notificationId)
        {
            await acceptUserNotificationService.Execute(new AcceptUserNotificationRequest { SteamId = steamId, NotificationId = notificationId });

            var notifications = await getUserNotificationsService.Execute(new GetUserNotificationsRequest { SteamId = steamId });

            await Clients.Caller.SendAsync("ReceiveNotifications", notifications);
        }

        public async Task RefuseNotificationAsync(string steamId, Guid notificationId)
        {
            await refuseUserNotificationService.Execute(new RefuseUserNotificationRequest { SteamId = steamId, NotificationId = notificationId });

            var notifications = await getUserNotificationsService.Execute(new GetUserNotificationsRequest { SteamId = steamId });

            await Clients.Caller.SendAsync("ReceiveNotifications", notifications);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, "notifications", Context.ConnectionAborted);

            return base.OnDisconnectedAsync(exception);
        }

        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, "notifications", Context.ConnectionAborted);

            return base.OnConnectedAsync();
        }
    }
}
