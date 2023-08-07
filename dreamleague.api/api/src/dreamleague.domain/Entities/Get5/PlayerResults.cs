namespace dreamleague.domain.Entities.Get5
{
    public class PlayerResults
    {
        public string SteamId { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public int Kills { get; set; }
        public int Assists { get; set; }
        public int Deaths { get; set; }
        public int RoundsPlayed { get; set; }
        public int Damage { get; set; }
        public decimal Adr { get => Damage / RoundsPlayed; }
        public decimal Kdr { get => Deaths == 0 ? Kills : Kills/Deaths; }
        public int Diff { get => Kills - Deaths; }
        public int S { get => RoundsPlayed - Deaths; }
    }
}
