using dreamleague.domain.Adapters.CreateMatch;
using dreamleague.domain.Aggregates.CreateMatch;
using dreamleague.common.Entities.Get5;
using dreamleague.common.Entities.Rcon;
using dreamleague.common.Entities.Servers;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Match;
using dreamleague.shared.Configurations;
using dreamleague.shared.Services;
using MongoDB.Bson;

namespace dreamleague.services.Services.Matches
{
    public class CreateMatchService : GenericService<CreateMatchRequest, CreateMatchResponse>, ICreateMatchService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICreateMatchAdapter adapter;
        private readonly ApplicationConfig applicationConfig;
        public CreateMatchService
            (
                IUnitOfWork unitOfWork,
                ICreateMatchAdapter adapter,
                ApplicationConfig applicationConfig
            )
        {
            this.unitOfWork = unitOfWork;
            this.adapter = adapter;
            this.applicationConfig = applicationConfig;
        }
        protected async override Task<CreateMatchResponse> OnExecute(CreateMatchRequest request)
        {
            var server = await unitOfWork.ServerRepository.GetFirstAvailableServerAsync();

            var availability = await unitOfWork.RconRepository.CheckAvailabilityAsync(server);

            if (availability.available != 1)
                throw new NotImplementedException("Server not available!");

            var firstPlayers = request.Players.Take(applicationConfig.Queue.MinPlayersPerTeam);
            var team1 = await unitOfWork.MatchRepository.CreateTeamAsync(adapter.ToTeamMatch(firstPlayers));

            var lastPlayers = request.Players.TakeLast(applicationConfig.Queue.MinPlayersPerTeam);
            var team2 = await unitOfWork.MatchRepository.CreateTeamAsync(adapter.ToTeamMatch(lastPlayers));

            request.Team1Id = team1.id;
            request.Team1String = team1.name;           
            request.Team2Id = team2.id;
            request.Team2String = team2.name;

            var match = await unitOfWork.MatchRepository.CreateMatchAsync(adapter.ToGet5Match(request), server.Id);

            var rconMatch = new RconMatch(match, team1, team2);

            await unitOfWork.MatchStorageRepository.CreateMatchJsonFileAsync(rconMatch);

            await unitOfWork.RconRepository.StartMatchInServerAsync(server, match.MatchId);

            await unitOfWork.RconRepository.SetGet5ApiKeyAsync(server, match.ApiKey);

            await unitOfWork.ServerRepository.UpdateServerStatusAsync(server.Id, true);

            return adapter.ToCreateMatchResponse(server, rconMatch);
        }
    }
}
