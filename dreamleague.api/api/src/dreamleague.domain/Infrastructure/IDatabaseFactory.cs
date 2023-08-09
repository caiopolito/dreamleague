using MongoDB.Driver;
using System.Data;

namespace dreamleague.domain.Infrastructure
{
    public interface IDatabaseFactory
    {
        IDbConnection GetConnection();
        IMongoDatabase GetMongoDatabase();
    }
}
