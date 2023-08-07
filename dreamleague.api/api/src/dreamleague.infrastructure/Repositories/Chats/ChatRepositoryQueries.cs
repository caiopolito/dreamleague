namespace dreamleague.infrastructure.Repositories.Chats
{
    public static class ChatRepositoryQueries
    {    
        public const string GetChatId = @"
            SELECT 
                [cp].chat_id as ChatId
            FROM
                chat_players [cp]
            WHERE
                [cp].player_id = @Player
                AND [cp].chat_id IN (SELECT chat_id FROM chat_players WHERE player_id = @Friend)
        ";

        public const string GetChatMessagesByChatId = @"
            SELECT 
                [m].id as Id,
                [m].chat_id as ChatId,
                [m].sender_steamid as Sender,
                [m].receiver_steamid as Receiver,
                [m].message as Content,
                [m].message_time as MessageTime
            FROM
                messages [m]
            WHERE
                [m].[chat_id] = @ChatId
            ORDER BY
                CONVERT(datetime, message_time) asc
        ";

        public const string CreateChat = @"
            BEGIN TRANSACTION T1

            INSERT INTO chats (id) VALUES (@ChatId)

            INSERT INTO chat_players (chat_id, player_id) 
            VALUES (@ChatId, @Player),
                (@ChatId, @Friend)

            COMMIT TRANSACTION T1
        ";

        public const string InsertMessage = @"
            BEGIN TRANSACTION T1

            INSERT INTO [messages] 
                (id, chat_id, sender_steamid, receiver_steamid, message, message_time) 
            VALUES 
                (NEWID(), @ChatId, @Sender, @Receiver, @Content, @MessageTime)

            SELECT 
                [m].id as Id,
                [m].chat_id as ChatId,
                [m].sender_steamid as Sender,
                [m].receiver_steamid as Receiver,
                [m].message as Content,
                [m].message_time as MessageTime
            FROM
                messages [m]
            WHERE
                [m].[chat_id] = @ChatId
            ORDER BY
                CONVERT(datetime, message_time) asc
            
            COMMIT TRANSACTION T1
        ";
    }
}
