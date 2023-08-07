namespace dreamleague.infrastructure.Repositories.Notifications
{
    public static class NotificationRepositoryQueries
    {
        public const string GetTeamNotifications = @"
            SELECT 
                [tn].id AS Id,
                [tn].steam_id AS SteamId,
                [tn].team_id AS TeamId,
                [t].name AS TeamName,
                [tn].is_responded AS IsResponded,
                [tn].responded_at AS RespondedAt,
                [tn].is_seen AS IsSeen,
                [tn].seen_at AS SeenAt
            FROM 
                [team_notifications] [tn]
            LEFT JOIN 
                [teams] [t] ON [tn].[team_id] = [t].id
            WHERE 
                [tn].[steam_id] = @SteamId
            AND
                [tn].[responded_at] is null
            AND
                [tn].[is_responded] = 0
        ";           
        
        public const string GetTeamNotificationById = @"
            SELECT 
                [tn].id AS Id,
                [tn].steam_id AS SteamId,
                [tn].team_id AS TeamId,
                [t].name AS TeamName,
                [tn].is_responded AS IsResponded,
                [tn].responded_at AS RespondedAt,
                [tn].is_seen AS IsSeen,
                [tn].seen_at AS SeenAt
            FROM [team_notifications] [tn]
            LEFT JOIN [teams] [t] ON [tn].[team_id] = [t].id
            WHERE [tn].[id] = @NotificationId
        ";              
        
        public const string SetTeamNotificationsAsSeen = @"
            UPDATE [team_notifications]
            SET
                is_seen = 1,
                seen_at = GETDATE()
            WHERE steam_id = @SteamId
        ";           
        
        public const string SetTeamNotificationAsResponded = @"
            UPDATE [team_notifications]
            SET
                is_responded = 1,
                responded_at = GETDATE()
            WHERE 
                steam_id = @SteamId
            AND 
                id = @NotificationId
        ";

        public const string DeleteNotificationsByTeamId = @"
            DELETE FROM [team_notifications]
            WHERE 
                team_id = @TeamId
        ";

        public const string CreateTeamNotification = @"
            INSERT INTO [team_notifications]
                (
                id,
                steam_id,
                team_id
                )
            OUTPUT
                Inserted.id AS Id,
                Inserted.steam_id AS SteamId,
                Inserted.team_id AS TeamId
            VALUES
                (
                NEWID(),
                @SteamId,
                @TeamId
                )
        ";
    }
}
