namespace dreamleague.shared.Configurations
{
    public class ApplicationConfig
    {
        public ApplicationConfig()
        {
            Apis = new ();
            Authentication = new ();
            ConnectionStrings = new ();
            Queue = new();
        }
        public string[]? CorsConfig { get; init; }
        public ApisConfig Apis { get; init; }
        public AuthenticationConfig Authentication { get; init; }
        public ConnectionStringsConfig ConnectionStrings { get; init; }
        public QueueConfig Queue { get; init; }
    }
}
