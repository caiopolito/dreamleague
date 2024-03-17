using Dapper;
using dreamleague.domain.Aggregates.GetOrCreateChatInfo;
using dreamleague.domain.Aggregates.SendMessage;
using dreamleague.common.Entities.Chat;
using dreamleague.domain.Infrastructure;


namespace dreamleague.infrastructure.Repositories.Chats
{
    public class ChatRepository : IChatRepository
    {
        private readonly IDatabaseFactory databaseFactory;
        public ChatRepository(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public async Task<bool> GetHealthCheckAsync()
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                return await connection.QueryFirstOrDefaultAsync<bool>(@"SELECT 1");
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<ChatInfo> GetChatInfoAsync(GetOrCreateChatInfoRequest request)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var chatInfo = await connection.QueryFirstOrDefaultAsync<ChatInfo>(ChatRepositoryQueries.GetChatId, new { request.Player, request.Friend });

                if (chatInfo != null)
                    chatInfo.Messages = await connection.QueryAsync<Message>(ChatRepositoryQueries.GetChatMessagesByChatId, new { chatInfo.ChatId });

                return chatInfo;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<ChatInfo> CreateChatAsync(GetOrCreateChatInfoRequest request)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new { request.Player, request.Friend, ChatId = Guid.NewGuid() };
                await connection.ExecuteAsync(sql: ChatRepositoryQueries.CreateChat, param: parameters);

                return new ChatInfo { ChatId = parameters.ChatId };
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<ChatInfo> InsertAndRetrieveMessagesAsync(SendMessageRequest request)
        {
            using var connection = databaseFactory.GetConnection();
            try
            {
                var parameters = new
                {
                    Content = request.Message,
                    request.Chat.ChatId,
                    request.Sender,
                    request.Receiver,
                    request.MessageTime
                };

                request.Chat.Messages = await connection.QueryAsync<Message>(sql: ChatRepositoryQueries.InsertMessage, param: parameters);

                return request.Chat;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
