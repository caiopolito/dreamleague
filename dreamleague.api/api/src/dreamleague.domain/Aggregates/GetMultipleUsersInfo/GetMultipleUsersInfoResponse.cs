using dreamleague.domain.Entities.Steam;
using dreamleague.domain.Entities.Steam.PlayerSummary;

namespace dreamleague.domain.Aggregates.GetMultipleUsersInfo
{
    public class GetMultipleUsersInfoResponse
    {
        public List<SteamPlayer> players { get; set; }

        public static implicit operator GetMultipleUsersInfoResponse (SteamResponses entity)
        {
            return new GetMultipleUsersInfoResponse
            {
                players = entity.response.players
            };
        }
    }
}
