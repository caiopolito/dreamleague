using dreamleague.domain.Infrastructure;

namespace dreamleague.infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public ISteamUserRepository SteamUserRepository { get; private set; }
        public IPlayerRepository PlayerRepository { get; private set; }
        public IChatRepository ChatRepository { get; private set; }
        public IServerRepository ServerRepository { get; private set; }
        public IMatchStorageRepository MatchStorageRepository { get; private set; }
        public IMatchRepository MatchRepository { get; private set; }
        public ITeamRepository TeamRepository { get; private set; }
        public IChampionshipRepository ChampionshipRepository { get; private set; }
        public IRconRepository RconRepository { get; private set; }
        public INotificationRepository NotificationRepository { get; private set; }

        public UnitOfWork
            (
            ISteamUserRepository steamUserRepository, 
            IPlayerRepository playerRepository, 
            IChatRepository chatRepository,
            IServerRepository serverRepository,
            IMatchStorageRepository matchStorageRepository,
            IMatchRepository matchRepository,
            ITeamRepository teamRepository,
            IChampionshipRepository championshipRepository,
            IRconRepository rconRepository,
            INotificationRepository notificationRepository
            )
        {
            SteamUserRepository = steamUserRepository;
            PlayerRepository = playerRepository;
            ChatRepository = chatRepository;
            ServerRepository = serverRepository;
            MatchStorageRepository = matchStorageRepository;
            MatchRepository = matchRepository;
            TeamRepository = teamRepository;
            ChampionshipRepository = championshipRepository;
            RconRepository = rconRepository;
            NotificationRepository = notificationRepository;
        }

    }
}
