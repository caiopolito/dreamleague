namespace dreamleague.shared.Configurations
{
    public class ConnectionStringsConfig
    {
        public ConnectionStringsConfig()
        {

        }
        public string? DefaultConnection { get; init; }
        public string? BlobStorageConnection { get; set; }
    }
}
