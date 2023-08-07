namespace dreamleague.infrastructure.Repositories.Matches
{
    public static class MatchRepositoryQueries
    {
        public const string CreateMatch = @"
            BEGIN TRANSACTION T1

            INSERT INTO [matches] 
                (
                match_title,
                server_id,
                api_key,
                team1_id,
                team2_id,
                team1_string,
                team2_string
                ) 
            OUTPUT 
                Inserted.matchid AS MatchId,
                Inserted.server_id AS ServerId,
                Inserted.api_key AS ApiKey,
                Inserted.team1_id AS Team1Id,
                Inserted.team2_id AS Team2Id,
                Inserted.team1_string AS Team1String,
                Inserted.team2_string AS Team2String
            VALUES 
                (
                @MatchTitle,
                @ServerId,
                @ApiKey,
                @Team1Id,
                @Team2Id,
                @Team1String,
                @Team2String
                )

            COMMIT TRANSACTION T1
        ";        
        
        public const string GetPlayerMatchesBySteamId = @"
            SELECT 
                [m].matchid AS MatchId,
                [m].match_title AS MatchTitle,
                [m].server_id AS ServerId,
                [m].team1_id AS Team1Id,
                [m].team2_id AS Team2Id,
                [m].team1_string AS Team1String,
                [m].team2_string AS Team2String,
                [m].winner AS Winner,
                [m].plugin_version AS PluginVersion,
                [m].forfeit AS Forfeit,
                [m].cancelled AS Cancelled,
                [m].start_time AS StartTime,
                [m].end_time AS EndTime,
                [m].max_maps AS MaxMaps,
                [m].skip_veto AS SkipVeto,
                [m].api_key AS ApiKey,
                [mp].team1_score AS Team1Score,
                [mp].team2_score AS Team2Score,
                [mp].map_name AS MapName,
                [ps].kills AS Kills,
                [ps].deaths AS Deaths,
                [ps].assists AS Assists
            FROM 
                [matches] [m]
            LEFT JOIN [map_stats] [mp] ON [m].matchid = [mp].[match_id]
            LEFT JOIN [player_stats] [ps] ON [ps].[steam_id] = @SteamId AND [ps].[match_id] = [mp].[match_id]
            WHERE
                ([m].team1_id in (SELECT DISTINCT
                                    [tm].[id]
                                FROM [team_match] [tm]
                                RIGHT JOIN [team_match_players] [tmp] ON [tm].[id] = [tmp].[team_match_id]
                                WHERE [tmp].[player_id] = @SteamId)
            OR [m].team2_id in (SELECT DISTINCT
                                    [tm].[id]
                                FROM [team_match] [tm]
                                RIGHT JOIN [team_match_players] [tmp] ON [tm].[id] = [tmp].[team_match_id]
                                WHERE [tmp].[player_id] = @SteamId))
            AND MONTH(GETDATE()) = MONTH([m].end_time)
            ORDER BY [m].end_time DESC
        ";

        public const string GetPlayersByMatchId = @"
            SELECT DISTINCT
                [tmp].player_id
            FROM 
                [team_match_players] [tmp]
            WHERE
                ([tmp].[team_match_id] = (SELECT 
                                            team1_id
                                        FROM matches
                                        WHERE matchid = @MatchId)
            OR [tmp].[team_match_id] = (SELECT 
                                            team2_id
                                        FROM matches
                                        WHERE matchid = @MatchId))
        ";        
        
        public const string GetPlayersByMatchAndTeamId = @"
            SELECT DISTINCT
                [tmp].player_id AS SteamId,
                [ps].name AS Name,
                [ps].kills AS Kills,
                [ps].deaths AS Deaths,
                [ps].assists AS Assists,
                [ps].damage AS Damage,
                [ps].roundsplayed AS RoundsPlayed,
                [p].avatar AS Avatar
            FROM 
                [team_match_players] [tmp]
            LEFT JOIN 
                [player_stats] [ps] ON [tmp].[player_id] = [ps].[steam_id] AND [ps].[match_id] = @MatchId
            LEFT JOIN 
                [players] [p] ON [tmp].[player_id] = [p].[steam_id]          
            WHERE
                ([tmp].[team_match_id] = (SELECT 
                                            team1_id
                                        FROM matches
                                        WHERE matchid = @MatchId)
            OR [tmp].[team_match_id] = (SELECT 
                                            team2_id
                                        FROM matches
                                        WHERE matchid = @MatchId))
            AND [ps].[team_id] = @TeamId
        ";

        public const string CreateMatchMapStats = @"
            INSERT INTO [map_stats] 
                (
                match_id,
                map_number,
                map_name,
                start_time
                ) 
            VALUES 
                (
                @MatchId,
                @MapNumber,
                @MapName,   
                GETDATE()   
                )
        ";

        public const string CreateMatchMapPlayerStats = @"
            INSERT INTO [player_stats]
                (
                match_id,
                map_id,
                steam_id
                )
            OUTPUT
                Inserted.id AS Id
            VALUES
                (
                @MatchId,
                @MapId,
                @SteamId
                )
        ";

        public const string UpdateMatchMapStats = @"
            UPDATE [map_stats] 
            SET
                team1_score = @Team1Score,
                team2_score = @Team2Score,
                end_time = @EndTime,
                winner = @Winner       
            WHERE
                match_id = @MatchId
            AND map_number = @MapNumber
        ";

        public const string GetMatchById = @"
            SELECT 
                [m].matchid AS MatchId,
                [m].match_title AS MatchTitle,
                [m].server_id AS ServerId,
                [m].team1_id AS Team1Id,
                [m].team2_id AS Team2Id,
                [m].team1_string AS Team1String,
                [m].team2_string AS Team2String,
                [m].winner AS Winner,
                [m].plugin_version AS PluginVersion,
                [m].forfeit AS Forfeit,
                [m].cancelled AS Cancelled,
                [m].start_time AS StartTime,
                [m].end_time AS EndTime,
                [m].max_maps AS MaxMaps,
                [m].skip_veto AS SkipVeto,
                [m].api_key AS ApiKey,
                [m].team1_score AS Team1Score,
                [m].team2_score AS Team2Score
            FROM
                [matches] [m]
            WHERE
                [m].matchid = @MatchId
        ";        
        
        public const string GetMatchMapPlayerStats = @"
            SELECT 
                [ps].id AS Id,
                [ps].match_id AS MatchId,
                [ps].map_id AS MapId,
                [ps].team_id AS TeamId,
                [ps].steam_id AS SteamId,
                [ps].name AS Name,
                [ps].kills AS Kills,
                [ps].deaths AS Deaths,
                [ps].roundsplayed AS RoundsPlayed,
                [ps].assists AS Assists,
                [ps].flashbang_assists AS FlashbangAssists,
                [ps].teamkills AS Teamkills,
                [ps].suicides AS Suicides,
                [ps].headshot_kills AS HeadshotKill,
                [ps].damage AS Damage,
                [ps].bomb_plants AS BombPlants,
                [ps].bomb_defuses AS BombDefuses,
                [ps].v1 AS v1,
                [ps].v2 AS v2,
                [ps].v3 AS v3,
                [ps].v4 AS v4,
                [ps].v5 AS v5,
                [ps].k1 AS k1,
                [ps].k2 AS k2,
                [ps].k3 AS k3,
                [ps].k4 AS k4,
                [ps].k5 AS k5,
                [ps].firstkill_t AS FirstKillT,
                [ps].firstkill_ct AS FirstKillCt,
                [ps].firstdeath_t AS FirstDeathT,
                [ps].firstdeath_ct AS FirstDeathCt
            FROM
                [player_stats] [ps]
            WHERE
                [ps].steam_id = @SteamId
            AND [ps].map_id = @MapId
            AND [ps].match_id = @MatchId
        ";

        public const string UpdateMatchMapPlayerStats = @"
            UPDATE [player_stats]
            SET
                team_id = @TeamId,
                name = @Name,
                kills = @Kills,
                deaths = @Deaths,
                roundsplayed = @RoundsPlayed,
                assists = @Assists,
                flashbang_assists = @FlashbangAssists,
                teamkills = @Teamkills,
                suicides = @Suicides,
                headshot_kills = @HeadshotKill,
                damage = @Damage,
                bomb_plants = @BombPlants,
                bomb_defuses = @BombDefuses,
                v1 = @v1,
                v2 = @v2,
                v3 = @v3,
                v4 = @v4,
                v5 = @v5,
                k1 = @k1,
                k2 = @k2,
                k3 = @k3,
                k4 = @k4,
                k5 = @k5,
                firstkill_t = @FirstKillT,
                firstkill_ct = @FirstKillCt,
                firstdeath_t = @FirstDeathT,
                firstdeath_ct = @FirstDeathCt
            WHERE
                match_id = @MatchId
            AND map_id = @MapId
            AND steam_id = @SteamId
        ";        
        

        public const string GetMatchMapStatsByMatchId = @"
            SELECT 
                [ms].id AS Id,
                [ms].match_id AS MatchId,
                [ms].map_number AS MapNumber,
                [ms].map_name AS MapName,
                [ms].start_time AS StartTime,
                [ms].end_time AS EndTime,
                [ms].winner AS Winner,
                [ms].team1_score AS Team1Score,
                [ms].team2_score AS Team2Score
            FROM
                [map_stats] [ms]
            WHERE
                [ms].match_id = @MatchId
            AND [ms].map_number = @MapNumber
        ";

        public const string UpdateMatch = @"
            BEGIN TRANSACTION T1

            UPDATE [matches] 
            SET 
                start_time = @StartTime,
                end_time = @EndTime,
                team1_score = @Team1Score,
                team2_score = @Team2Score,
                winner = @Winner,
                forfeit = @Forfeit,
                cancelled = @Cancelled
            WHERE
                matchid = @MatchId

            COMMIT TRANSACTION T1
        ";

        public const string CreateTeam = @"
            BEGIN TRANSACTION T1

            INSERT INTO [team_match] 
                (
                name,
                tag,
                flag,
                logo
                ) 
            OUTPUT
                Inserted.id,
                Inserted.name,
                Inserted.tag,
                Inserted.flag,
                Inserted.logo
            VALUES 
                (
                @Name,
                @Tag,
                @Flag,
                @Logo
                )

            COMMIT TRANSACTION T1
        ";
        public const string CreatePlayerTeam = @"
            BEGIN TRANSACTION T1

            INSERT INTO [team_match_players] 
                (
                team_match_id,
                player_id
                ) 
            VALUES 
                (
                @TeamId,
                @PlayerId
                )

            COMMIT TRANSACTION T1
        ";
    }
}
