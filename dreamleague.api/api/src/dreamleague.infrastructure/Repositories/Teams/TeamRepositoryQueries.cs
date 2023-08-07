namespace dreamleague.infrastructure.Repositories.Teams
{
    public static class TeamRepositoryQueries
    {
        public const string InsertAndReturnTeam = @"
            BEGIN TRANSACTION T1

            INSERT INTO [teams]
                (id, name) 
            OUTPUT
                Inserted.id as Id,
                Inserted.name as Name
            VALUES 
                (NEWID(), @Name)
            
            COMMIT TRANSACTION T1
        ";

        public const string DeleteAllPlayersFromTeam = @"
            DELETE FROM [team_players]
            WHERE 
                [team_id] = @TeamId
        ";        
        
        public const string DeletePlayer = @"
            DELETE FROM [team_players]
            WHERE 
                [team_id] = @TeamId
            AND [player_id] = @SteamId
        ";

        public const string InsertPlayerIntoTeam = @"
            INSERT INTO [team_players]
                (team_id, player_id, is_captain) 
            VALUES 
                (@TeamId, @PlayerId, @IsCaptain)
        ";


        public const string DeleteTeamById = @"
            DELETE FROM [teams] 
            WHERE
                [id] = @TeamId 
        ";

        public const string UpdateTeamById = @"
            UPDATE [teams]
            SET 
                name = @Name
            OUTPUT
                Inserted.id as Id,
                Inserted.name as Name
            WHERE
                [id] = @TeamId
        ";

        public const string GetTeamById = @"
            SELECT 
                [t].id as Id,
                [t].name as Name
            FROM
                teams [t]
            WHERE
                [t].[id] = @TeamId
        ";

        public const string GetTeams = @"
            SELECT DISTINCT 
                  [t].id AS Id,
                  [t].name AS Name
            FROM 
                teams [t]
            RIGHT JOIN 
                team_players [tp] ON [t].id = [tp].team_id
            WHERE 
                [tp].[player_id] = @SteamId
            AND [tp].[is_captain] in @IsCaptain
        ";


        public const string GetTeamPlayers = @"
            SELECT 
                [tp].team_id AS TeamId,
                [p].steam_id AS SteamId,
                [p].name AS Name,
                [tp].is_captain AS IsCaptain,
                [p].avatar AS Avatar
            FROM
                team_players [tp]
            LEFT JOIN
                players [p] on [tp].[player_id] = [p].[steam_id]
            WHERE
                [tp].[team_id] = @TeamId
        ";
    }
}
