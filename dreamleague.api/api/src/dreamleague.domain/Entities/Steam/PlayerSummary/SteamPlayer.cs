namespace dreamleague.domain.Entities.Steam.PlayerSummary
{
    public class SteamPlayer
    {
        public SteamPlayer()
        {

        }
        public string? steamid { get; set; }
        public int? communityvisibilitystate { get; set; }
        public int? profilestate { get; set; }
        public string? personaname { get; set; }
        public int? commentpermission { get; set; }
        public Uri? profileurl { get; set; }
        public Uri? avatar { get; set; }
        public Uri? avatarmedium { get; set; }
        public Uri? avatarfull { get; set; }
        public string? avatarhash { get; set; }
        public long? lastlogoff { get; set; }
        public int? personastate { get; set; }
        public string? realname { get; set; }
        public string? primaryclanid { get; set; }
        public long? timecreated { get; set; }
        public int? personastateflags { get; set; }
        public string? loccountrycode { get; set; }
        public string? locstatecode { get; set; }
    }
}
