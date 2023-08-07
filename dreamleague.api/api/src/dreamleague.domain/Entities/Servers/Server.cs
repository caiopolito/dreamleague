namespace dreamleague.domain.Entities.Servers
{
    public class Server
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string RconPassword { get; set; }
        public string Password { get; set; }
        public bool InUse { get; set; }
        public string ConcatedIp { get => IpAddress + ":" + Port; }
    }
}
