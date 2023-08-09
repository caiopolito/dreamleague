namespace dreamleague.domain.Infrastructure
{
    public interface IUnitOfWork
    {
        public ISteamUserRepository SteamUserRepository { get; }
        public IPlayerRepository PlayerRepository { get; }
        public IChatRepository ChatRepository { get; }
        public IServerRepository ServerRepository { get; }
        public IMatchStorageRepository MatchStorageRepository { get; }
        public IMatchRepository MatchRepository { get; }
        public ITeamRepository TeamRepository { get; }
        public IChampionshipRepository ChampionshipRepository { get; }
        public IRconRepository RconRepository { get; }
        public INotificationRepository NotificationRepository { get; }
    }
}
