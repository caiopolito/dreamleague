namespace dreamleague.shared.Configurations
{
    public class ConnectionStringsConfig
    {
        public ConnectionStringsConfig()
        {

        }
        public string? DefaultConnection { get; init; }
        public string? MongoConnection { get; init; }
        public string? MongoDatabaseName { get; init; }
    }
}
