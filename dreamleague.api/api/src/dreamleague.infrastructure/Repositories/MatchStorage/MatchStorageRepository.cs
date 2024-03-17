using dreamleague.common.Entities.Rcon;
using dreamleague.domain.Infrastructure;

namespace dreamleague.infrastructure.Repositories.MatchStorage
{
    public class MatchStorageRepository : IMatchStorageRepository
    {
        private readonly IDatabaseFactory databaseFactory;
        private const string collectionName = @"matches";
        public MatchStorageRepository
            (
                IDatabaseFactory databaseFactory            
            )
        {
            this.databaseFactory = databaseFactory;
        }
        public async Task CreateMatchJsonFileAsync(RconMatch match)
        {
            var database = databaseFactory.GetMongoDatabase();
            var collection = database.GetCollection<RconMatch>(collectionName);

            await collection.InsertOneAsync(match);
        }
    }
}
