namespace dreamleague.infrastructure.Repositories.Championships
{
    public static class ChampionshipRepositoryQueries
    {
        public const string InsertAndReturnChampionship = @"
            BEGIN TRANSACTION T1

            INSERT INTO [championships] 
                (id, name, description, start_date, end_date, min_teams, players_on_team) 
            OUTPUT
                Inserted.id as Id,
                Inserted.name as Name,
                Inserted.description as Description,
                Inserted.start_date as StartDate,
                Inserted.end_date as EndDate,
                Inserted.min_teams as MinTeams,
                Inserted.players_on_team as PlayersOnTeam
            VALUES 
                (NEWID(), @Name, @Description, @StartDate, @StartDate, @MinTeams, @PlayersOnTeam)
            
            COMMIT TRANSACTION T1
        ";

        public const string RegisterTeam = @"
            BEGIN TRANSACTION T1

            INSERT INTO [championship_teams] 
                (championship_id, team_id) 
            OUTPUT
                Inserted.championship_id as ChampionshipId,
                Inserted.team_id as TeamId
            VALUES 
                (@ChampionshipId, @TeamId)
            
            COMMIT TRANSACTION T1
        ";

        public const string RemoveTeam = @"
            DELETE FROM [championship_teams]
            WHERE championship_id = @ChampionshipId
            AND team_id = @TeamId
        ";

        public const string GetAvailableChampionships = @"
            SELECT 
                [c].id as Id,
                [c].name as Name,
                [c].description as Description,
                [c].start_date as StartDate,
                [c].end_date as EndDate,
                [c].min_teams as MinTeams,
                [c].players_on_team as PlayersOnTeam
            FROM
                championships [c]
            WHERE
                [c].[end_date] > GETDATE()
        ";

        public const string DeleteChampionshipById = @"
            DELETE FROM [championships] 
            WHERE
                [id] = @ChampionshipId 
        ";

        public const string UpdateChampionshipById = @"
            BEGIN TRANSACTION T1

            UPDATE [championships]
            SET 
                name = @Name,
                description = @Description,
                start_date = @StartDate,
                end_date = @StartDate,
                min_teams = @MinTeams,
                players_on_team = @PlayersOnTeam
            OUTPUT
                Inserted.id as Id,
                Inserted.name as Name,
                Inserted.description as Description,
                Inserted.start_date as StartDate,
                Inserted.end_date as EndDate,
                Inserted.min_teams as MinTeams,
                Inserted.players_on_team as PlayersOnTeam
            WHERE
                [id] = @ChampionshipId
            
            COMMIT TRANSACTION T1
        ";

        public const string GetChampionshipById = @"
            SELECT 
                [c].id as Id,
                [c].name as Name,
                [c].description as Description,
                [c].start_date as StartDate,
                [c].end_date as EndDate,
                [c].min_teams as MinTeams,
                [c].players_on_team as PlayersOnTeam
            FROM
                championships [c]
            WHERE
                [c].[id] = @ChampionshipId
        ";
        
        public const string GetChampionshipTeams = @"
            SELECT DISTINCT 
                  [t].id AS Id,
                  [t].name AS Name
            FROM 
                teams [t]
            LEFT JOIN championship_teams [ct] on [ct].[team_id] = [t].[id]
            WHERE 
                championship_id = @ChampionshipId
        ";
    }
}
