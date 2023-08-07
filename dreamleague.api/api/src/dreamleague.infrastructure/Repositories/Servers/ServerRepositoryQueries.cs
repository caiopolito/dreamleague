namespace dreamleague.infrastructure.Repositories.Servers
{
    public static class ServerRepositoryQueries
    {
        public const string GetAvailableServers = @"
            SELECT 
                [s].id as Id,
                [s].display_name as DisplayName,
                [s].ip_string as IpAddress,
                [s].port as Port,
                [s].rcon_password as RconPassword,
                [s].in_use as InUse,
                [s].password as Password
            FROM
                servers [s]
            WHERE
                [s].[in_use] = 0
        ";

        public const string GetServerById = @"
            SELECT 
                [s].id as Id,
                [s].display_name as DisplayName,
                [s].ip_string as IpAddress,
                [s].port as Port,
                [s].rcon_password as RconPassword,
                [s].in_use as InUse,
                [s].password as Password
            FROM
                servers [s]
            WHERE
                [s].[id] = @ServerId
        ";

        public const string UpdateServerStatus = @"
            UPDATE [servers]
            SET
                [in_use] = @Status
            WHERE
                [id] = @ServerId
        ";
    }
}
