using dreamleague.common.Entities.Get5;
using dreamleague.domain.Aggregates.FinishMatch;
using dreamleague.domain.Aggregates.FinishMatchMap;
using dreamleague.domain.Aggregates.MatchEvent;
using dreamleague.domain.Aggregates.StartMatchMap;
using dreamleague.domain.Aggregates.UpdateMatchMap;
using dreamleague.domain.Aggregates.UpdateMatchMapPlayer;
using dreamleague.domain.Services.Match;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Matches
{
    public class MatchEventService : GenericService<MatchEventRequest, object>, IMatchEventService
    {
        private readonly IFinishMatchService finishMatchService;
        private readonly IFinishMatchMapService finishMatchMapService;
        private readonly IStartMatchMapService startMatchMapService;
        private readonly IUpdateMatchMapService updateMatchMapService;
        private readonly IUpdateMatchMapPlayerService updateMatchMapPlayerService;

        public MatchEventService(
            IFinishMatchService finishMatchService,
            IFinishMatchMapService finishMatchMapService,
            IStartMatchMapService startMatchMapService,
            IUpdateMatchMapService updateMatchMapService,
            IUpdateMatchMapPlayerService updateMatchMapPlayerService)
        {
            this.finishMatchService = finishMatchService;
            this.finishMatchMapService = finishMatchMapService;
            this.startMatchMapService = startMatchMapService;
            this.updateMatchMapService = updateMatchMapService;
            this.updateMatchMapPlayerService = updateMatchMapPlayerService;
        }

        protected override async Task<object> OnExecute(MatchEventRequest request)
        {
            switch (MatchZyEventTypeParser.Parse(request.Event))
            {
                case MatchZyEventType.GoingLive:
                    await startMatchMapService.Execute(new StartMatchMapRequest
                    {
                        MatchId = request.MatchId,
                        MapNumber = request.MapNumber,
                        MapName = string.Empty,
                    });
                    break;

                case MatchZyEventType.RoundEnd:
                    await updateMatchMapService.Execute(new UpdateMatchMapRequest
                    {
                        MatchId = request.MatchId,
                        MapNumber = request.MapNumber,
                        Team1Score = request.Team1?.Score ?? 0,
                        Team2Score = request.Team2?.Score ?? 0,
                    });

                    await UpdatePlayersAsync(request, "team1", request.Team1?.Players);
                    await UpdatePlayersAsync(request, "team2", request.Team2?.Players);
                    break;

                case MatchZyEventType.MapResult:
                    await finishMatchMapService.Execute(new FinishMatchMapRequest
                    {
                        MatchId = request.MatchId,
                        MapNumber = request.MapNumber,
                        Winner = request.Winner?.Team ?? "none",
                    });
                    break;

                case MatchZyEventType.SeriesEnd:
                    string winner = request.Winner?.Team ?? "none";
                    bool cancelled = winner == "none";
                    await finishMatchService.Execute(new FinishMatchRequest
                    {
                        MatchId = request.MatchId,
                        Winner = winner,
                        Forfeit = cancelled ? 1 : 0,
                    });
                    break;
            }

            return new { };
        }

        private async Task UpdatePlayersAsync(MatchEventRequest request, string team, List<MatchEventPlayer>? players)
        {
            if (players == null) return;

            foreach (var player in players)
            {
                var stats = player.Stats;
                if (stats == null) continue;

                await updateMatchMapPlayerService.Execute(new UpdateMatchMapPlayerRequest
                {
                    MatchId = request.MatchId,
                    MapNumber = request.MapNumber,
                    SteamId = player.SteamId,
                    team = team,
                    name = player.Name,
                    kills = stats.Kills,
                    deaths = stats.Deaths,
                    assists = stats.Assists,
                    flashbang_assists = stats.FlashAssists,
                    teamkills = stats.TeamKills,
                    suicides = stats.Suicides,
                    damage = stats.Damage,
                    util_damage = stats.UtilityDamage,
                    enemies_flashed = stats.EnemiesFlashed,
                    friendlies_flashed = stats.FriendliesFlashed,
                    knife_kills = stats.KnifeKills,
                    headshot_kills = stats.HeadshotKills,
                    roundsplayed = stats.RoundsPlayed,
                    bomb_plants = stats.BombPlants,
                    bomb_defuses = stats.BombDefuses,
                    k1 = stats.K1,
                    k2 = stats.K2,
                    k3 = stats.K3,
                    k4 = stats.K4,
                    k5 = stats.K5,
                    v1 = stats.V1,
                    v2 = stats.V2,
                    v3 = stats.V3,
                    v4 = stats.V4,
                    v5 = stats.V5,
                    firstkill_t = stats.FirstKillsT,
                    firstkill_ct = stats.FirstKillsCt,
                    firstdeath_t = stats.FirstDeathsT,
                    firstdeath_ct = stats.FirstDeathsCt,
                    tradekill = stats.TradeKills,
                    kast = stats.Kast,
                    contribution_score = stats.Score,
                    mvp = stats.Mvp,
                });
            }
        }
    }
}
