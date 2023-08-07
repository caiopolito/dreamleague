using dreamleague.domain.Aggregates.GetOrCreateChatInfo;
using dreamleague.domain.Aggregates.SendMessage;
using dreamleague.domain.Entities.Chat;
using dreamleague.domain.Services.Chat;
using Microsoft.AspNetCore.SignalR;

namespace dreamleague.hubs.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IDictionary<string, ChatInfo> chats;

        private readonly IGetOrCreateChatInfoService getOrCreateChatInfoService;
        private readonly ISendMessageService sendMessageService;
        public ChatHub
            (
                IDictionary<string, ChatInfo> chats,
                IGetOrCreateChatInfoService getOrCreateChatInfoService,
                ISendMessageService sendMessageService
            )
        {
            this.chats = chats;
            this.getOrCreateChatInfoService = getOrCreateChatInfoService;
            this.sendMessageService = sendMessageService;
        }

        public async Task EnterChatAsync(string player, string friend)
        {
            try
            {
                var connectionId = Context.ConnectionId;

                var request = new GetOrCreateChatInfoRequest
                {
                    Player = player,
                    Friend = friend
                };

                var chatInfo = await getOrCreateChatInfoService.Execute(request);

                await Groups.AddToGroupAsync(connectionId, chatInfo.ChatId.ToString(), Context.ConnectionAborted);

                chats[connectionId] = chatInfo;

                await Clients.Group(chatInfo.ChatId.ToString()).SendAsync("ReceiveMessage", chatInfo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SendMessageAsync(string message, string receiver, string sender)
        {
            if (chats.TryGetValue(Context.ConnectionId, out ChatInfo chatInfo))
            {
                var request = new SendMessageRequest
                {
                    Chat = chatInfo,
                    Message = message,
                    Receiver = receiver,
                    Sender = sender
                };

                var response = await sendMessageService.Execute(request);

                await Clients.Group(chatInfo.ChatId.ToString()).SendAsync("ReceiveMessage", response);
            }
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (chats.TryGetValue(Context.ConnectionId, out ChatInfo chatInfo))
            {
                chats.Remove(Context.ConnectionId);
                Groups.RemoveFromGroupAsync(Context.ConnectionId, chatInfo.ChatId.ToString(), Context.ConnectionAborted);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
