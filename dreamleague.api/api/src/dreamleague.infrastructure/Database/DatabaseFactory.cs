using dreamleague.domain.Infrastructure;
using dreamleague.shared.Configurations;
using System.Data;
using System.Data.SqlClient;

namespace dreamleague.infrastructure.Database
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly ApplicationConfig _config;

        public DatabaseFactory(ApplicationConfig config)
        {
            _config = config;
        }

        public IDbConnection GetConnection() => new SqlConnection(_config.ConnectionStrings.DefaultConnection);
    }
}
