namespace dreamleague.domain.Entities.Servers
{
    public class ServerConnection
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string ConcatedIp { get => IpAddress + ":" + Port; }

    }
}
