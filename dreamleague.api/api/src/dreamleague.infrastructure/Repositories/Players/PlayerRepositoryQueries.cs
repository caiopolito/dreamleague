namespace dreamleague.infrastructure.Repositories.Players
{
    public static class PlayerRepositoryQueries
    {
        public const string GetPlayerInfoBySteamId = @"
            SELECT 
                [p].steam_id AS SteamId,
                [p].coins AS Coins,
                [p].points AS Points,
                [p].name AS Name,
                [p].is_admin AS IsAdmin
            FROM
                players [p]
            WHERE
                [p].steam_id = @steamId
        ";
        
        public const string CheckIfHasTeam = @"
            SELECT
                CASE WHEN EXISTS 
                (
                    SELECT 
                      * 
                    FROM team_players 
                    WHERE player_id = @SteamId
                    AND is_captain = 1
                )
                THEN 'TRUE'
                ELSE 'FALSE'
            END
        ";

        public const string GetAllPlayers = @"
            SELECT 
                [p].steam_id AS SteamId,
                [p].coins AS Coins,
                [p].points AS Points,
                [p].name AS Name,
                [p].is_admin AS IsAdmin,
                [p].avatar AS Avatar
            FROM
                players [p]
            WHERE
                [p].name LIKE '%' + @name + '%'
            AND [p].points >= @points  
            AND [p].steam_id NOT IN @notIn
        ";


        public const string InsertAndReturnFirstTimePlayerInfo = @"
            BEGIN TRANSACTION T1

            INSERT INTO [players]
                (steam_id, coins, points, name, avatar)
            VALUES 
                (@steamId, 100, 0, @name, @avatar)

            SELECT 
                [p].steam_id AS SteamId,
                [p].coins AS Coins,
                [p].points AS Points,
                [p].name AS Name,
                [p].is_admin AS IsAdmin,
                [p].avatar AS Avatar
            FROM
                players [p]
            WHERE
                [p].steam_id = @steamId
            
            COMMIT TRANSACTION T1
        ";

        public const string UpdateNameAndReturnPlayer = @"
            BEGIN TRANSACTION T1

            UPDATE 
                [players] 
            SET 
                name = @name,
                avatar = @avatar
            WHERE
                steam_id = @steamId

            SELECT 
                [p].steam_id AS SteamId,
                [p].coins AS Coins,
                [p].points AS Points,
                [p].name AS Name,
                [p].is_admin AS IsAdmin,
                [p].avatar AS Avatar
            FROM
                players [p]
            WHERE
                [p].steam_id = @steamId
            
            COMMIT TRANSACTION T1
        ";

        public const string UpdatePlayer = @"
            BEGIN TRANSACTION T1

            UPDATE 
                [players] 
            SET 
                coins = coins + @Coins,
                points = points + @Points
            WHERE
                steam_id = @SteamId
            
            COMMIT TRANSACTION T1
        ";
    }
}
