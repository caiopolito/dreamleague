using dreamleague.domain.Aggregates.CreateMatch;
using dreamleague.common.Entities.Matches;
using dreamleague.common.Entities.Players;
using dreamleague.common.Entities.Servers;
using dreamleague.domain.Services.Match;
using dreamleague.shared.Configurations;
using Microsoft.AspNetCore.SignalR;

namespace dreamleague.hubs.Hubs
{
    public class QueueHub : Hub
    {
        private readonly string queueName;
        private readonly int minPlayers;
        private readonly IDictionary<string, Player> playersInQueue;
        private readonly IDictionary<string, OngoingMatch> playersInMatch;
        private readonly ICreateMatchService createMatchService;
        public QueueHub
            (
                IDictionary<string, Player> playersInQueue,
                IDictionary<string, OngoingMatch> playersInMatch,
                ICreateMatchService createMatchService,
                ApplicationConfig appConfig
            )
        {
            this.playersInQueue = playersInQueue;
            this.playersInMatch = playersInMatch;
            this.createMatchService = createMatchService;
            queueName = appConfig.Queue.Name;
            minPlayers = appConfig.Queue.MinPlayers;
        }
        public async Task OnConnectAsync(string steamId)
        {
            if (playersInMatch.TryGetValue(steamId, out OngoingMatch ongoingMatch))
                await Clients.Caller.SendAsync("MatchGoingOn", $"connect {ongoingMatch.Server.ConcatedIp}; password {ongoingMatch.Server.Password};");
        }

        public async Task LeaveQueueAsync()
        {
            if (playersInQueue.TryGetValue(Context.ConnectionId, out Player player))
                playersInQueue.Remove(Context.ConnectionId);
        }
        public async Task EnterQueueAsync(string steamId, string name)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, queueName, Context.ConnectionAborted);
            if (!playersInQueue.Values.Any(x => x.SteamId == steamId) && !playersInMatch.Keys.Contains(steamId))
            {
                playersInQueue[Context.ConnectionId] = new Player { SteamId = steamId, Name = name };

                await Clients.Group(queueName).SendAsync("QueueInProgress", playersInQueue.Count, minPlayers);

                if (playersInQueue.Count == minPlayers)
                {
                    var keys = playersInQueue.Keys.ToList();

                    var match = await createMatchService.Execute(new CreateMatchRequest { Players = playersInQueue });

                    foreach (var item in keys)
                    {
                        playersInMatch[playersInQueue[item].SteamId] = new OngoingMatch { Match = match.Match, Server = match.Server };
                        await Groups.AddToGroupAsync(item, match.Match.matchid);
                        await Groups.RemoveFromGroupAsync(item, queueName);
                        playersInQueue.Remove(item);
                    }

                    await Clients.Group(match.Match.matchid).SendAsync("MatchStart", $"connect {match.Server.ConcatedIp}; password {match.Server.Password};");
                }
            }
            else if (playersInMatch.Keys.Contains(steamId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, queueName, Context.ConnectionAborted);
                await Groups.AddToGroupAsync(Context.ConnectionId, playersInMatch[steamId].Match.matchid, Context.ConnectionAborted);
                await Clients.Group(playersInMatch[steamId].Match.matchid).SendAsync("MatchGoingOn", $"connect {playersInMatch[steamId].Server.ConcatedIp}; password {playersInMatch[steamId].Server.Password};");
            }
            else
            {
                await Clients.Group(queueName).SendAsync("QueueInProgress", playersInQueue.Count, minPlayers);
            }
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (playersInQueue.TryGetValue(Context.ConnectionId, out Player player))
            {
                playersInQueue.Remove(Context.ConnectionId);
                Groups.RemoveFromGroupAsync(Context.ConnectionId, queueName, Context.ConnectionAborted);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
