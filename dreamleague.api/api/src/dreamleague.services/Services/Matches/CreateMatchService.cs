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
        public CreateMatchService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected async override Task<CreateMatchResponse> OnExecute(CreateMatchRequest request)
        {
            var server = await unitOfWork.ServerRepository.GetFirstAvailableServerAsync();

            var availability = await unitOfWork.RconRepository.CheckAvailabilityAsync(server);

            if (availability.available != 1)
                throw new NotImplementedException();

            var firstPlayers = request.Players.Take(1);
            var team1 = await unitOfWork.MatchRepository.CreateTeamAsync(new TeamMatch
            {
                name = "team " + firstPlayers.First().Value.Name,
                tag = "T1",
                players = firstPlayers.ToDictionary(item => item.Value.SteamId, item => item.Value.Name)
            });

            var lastPlayers = request.Players.TakeLast(1);
            var team2 = await unitOfWork.MatchRepository.CreateTeamAsync(new TeamMatch
            {
                name = "team " + lastPlayers.First().Value.Name,
                tag = "T2",
                players = lastPlayers.ToDictionary(item => item.Value.SteamId, item => item.Value.Name)
            });
            request.Team1Id = team1.id;
            request.Team1String = team1.name;           
            request.Team2Id = team2.id;
            request.Team2String = team2.name;
            var match = await unitOfWork.MatchRepository.CreateMatchAsync(request, server.Id);

            RconMatch rconMatch = request;
            rconMatch.matchid = match.MatchId;
            rconMatch.server_id = match.ServerId;

            //await unitOfWork.AzureBlobStorageRepository.UploadJsonFileAsync(rconMatch, rconMatch.json_file_name);

            await unitOfWork.RconRepository.StartMatchInServerAsync(server, rconMatch.json_file_name);

            await unitOfWork.RconRepository.SetGet5ApiKeyAsync(server, match.ApiKey);

            await unitOfWork.ServerRepository.UpdateServerStatusAsync(server.Id, true);

            return new CreateMatchResponse
            {
                Server = new ServerConnection
                {
                    IpAddress = server.IpAddress,
                    Port = server.Port,
                    Password = server.Password,
                },
                Match = rconMatch
            };
        }
    }
}
