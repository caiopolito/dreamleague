namespace dreamleague.common.Entities.Get5
{
    public class PlayerStats
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int MapId { get; set; }
        public Guid TeamId { get; set; }
        public string SteamId { get; set; }
        public string Name { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int RoundsPlayed { get; set; }
        public int Assists { get; set; }
        public int FlashbangAssists { get; set; }
        public int Teamkills { get; set; }
        public int Suicides { get; set; }
        public int HeadshotKill { get; set; }
        public int Damage { get; set; }
        public int BombPlants { get; set; }
        public int BombDefuses { get; set; }
        public int v1 { get; set; }
        public int v2 { get; set; }
        public int v3 { get; set; }
        public int v4 { get; set; }
        public int v5 { get; set; }
        public int k1 { get; set; }
        public int k2 { get; set; }
        public int k3 { get; set; }
        public int k4 { get; set; }
        public int k5 { get; set; }
        public int FirstKillT { get; set; }
        public int FirstKillCt { get; set; }
        public int FirstDeathT { get; set; }
        public int FirstDeathCt { get; set; }
    }
}
