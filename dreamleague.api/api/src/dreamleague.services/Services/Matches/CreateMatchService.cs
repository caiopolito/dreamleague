using dreamleague.domain.Adapters.CreateMatch;
using dreamleague.domain.Aggregates.CreateMatch;
using dreamleague.domain.Entities.Get5;
using dreamleague.domain.Entities.Rcon;
using dreamleague.domain.Entities.Servers;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Match;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Matches
{
    public class CreateMatchService : GenericService<CreateMatchRequest, CreateMatchResponse>, ICreateMatchService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICreateMatchAdapter adapter;
        public CreateMatchService
            (
                IUnitOfWork unitOfWork,
                ICreateMatchAdapter adapter
            )
        {
            this.unitOfWork = unitOfWork;
            this.adapter = adapter;
        }
        protected async override Task<CreateMatchResponse> OnExecute(CreateMatchRequest request)
        {
            var server = await unitOfWork.ServerRepository.GetFirstAvailableServerAsync();

            var availability = await unitOfWork.RconRepository.CheckAvailabilityAsync(server);

            if (availability.available != 1)
                throw new NotImplementedException("Server not available!");

            var firstPlayers = request.Players.Take(1);
            var team1 = await unitOfWork.MatchRepository.CreateTeamAsync(adapter.ToTeamMatch(firstPlayers));

            var lastPlayers = request.Players.TakeLast(1);
            var team2 = await unitOfWork.MatchRepository.CreateTeamAsync(adapter.ToTeamMatch(lastPlayers));

            request.Team1Id = team1.id;
            request.Team1String = team1.name;           
            request.Team2Id = team2.id;
            request.Team2String = team2.name;

            var match = await unitOfWork.MatchRepository.CreateMatchAsync(request, server.Id);

            var rconMatch = new RconMatch(match, team1, team2);

            await unitOfWork.MatchStorageRepository.CreateMatchJsonFileAsync(rconMatch);

            await unitOfWork.RconRepository.StartMatchInServerAsync(server, "filename // To Do (remove this)");

            await unitOfWork.RconRepository.SetGet5ApiKeyAsync(server, match.ApiKey);

            await unitOfWork.ServerRepository.UpdateServerStatusAsync(server.Id, true);

            return adapter.ToCreateMatchResponse(server, rconMatch);
        }
    }
}
