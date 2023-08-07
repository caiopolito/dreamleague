using CoreRCON;
using CoreRCON.Parsers.Standard;
using dreamleague.domain.Entities.Rcon;
using dreamleague.domain.Entities.Servers;
using dreamleague.domain.Infrastructure;
using dreamleague.shared.Configurations;
using Newtonsoft.Json;
using System.Net;

namespace dreamleague.infrastructure.Repositories.Rcon
{
    public class RconRepository : IRconRepository
    {
        private readonly ApplicationConfig appConfig;
        public RconRepository
            (
                ApplicationConfig appConfig
            )
        {
            this.appConfig = appConfig;
        }

        public async Task<Status> CheckConnectionAsync(Server server)
        {
            var rcon = new RCON(IPAddress.Parse(server.IpAddress), (ushort)server.Port, server.RconPassword);
            await rcon.ConnectAsync();

            return await rcon.SendCommandAsync<Status>("status");
        }

        public async Task<RconAvailable> CheckAvailabilityAsync(Server server)
        {
            var rcon = new RCON(IPAddress.Parse(server.IpAddress), (ushort)server.Port, server.RconPassword);
            await rcon.ConnectAsync();

            var response = await rcon.SendCommandAsync("get5_web_available");

            return JsonConvert.DeserializeObject<RconAvailable>(response.Substring(0, response.IndexOf($"L {DateTime.Today:MM/dd/yyyy}")));
        }

        public async Task StartMatchInServerAsync(Server server, string fileName)
        {
            var rcon = new RCON(IPAddress.Parse(server.IpAddress), (ushort)server.Port, server.RconPassword);
            await rcon.ConnectAsync();

            await rcon.SendCommandAsync($"get5_loadmatch_url {appConfig.Apis.BlobStorage}{fileName}.json");
        }

        public async Task SetGet5ApiKeyAsync(Server server, string apiKey)
        {
            var rcon = new RCON(IPAddress.Parse(server.IpAddress), (ushort)server.Port, server.RconPassword);
            await rcon.ConnectAsync();

            await rcon.SendCommandAsync($"get5_web_api_key {apiKey}");
        }
    }
}
