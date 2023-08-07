using dreamleague.domain.Aggregates.CheckIfHasTeam;
using dreamleague.domain.Aggregates.GetMultipleUsersInfo;
using dreamleague.domain.Aggregates.GetUserFriends;
using dreamleague.domain.Aggregates.GetUserInfo;
using dreamleague.domain.Aggregates.GetUserMatches;
using dreamleague.domain.Aggregates.GetUsers;
using dreamleague.domain.Enums.Ranks;
using dreamleague.domain.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace dreamleague.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IGetMultipleUsersInfoService getMultipleUsersInfoService;
        private readonly IGetUserInfoService getUserInfoService;
        private readonly IGetUserFriendsService getUserFriendsService;
        private readonly IGetUsersService getUsersService;
        private readonly IGetUserMatchesService getUserMatchesService;
        private readonly ICheckIfHasTeamService checkIfHasTeamService;
        public UsersController(
            IGetMultipleUsersInfoService getMultipleUsersInfoService,
            IGetUserInfoService getUserInfoService,
            IGetUserFriendsService getUserFriendsService,
            IGetUsersService getUsersService,
            IGetUserMatchesService getUserMatchesService,
            ICheckIfHasTeamService checkIfHasTeamService
            )
        {
            this.getMultipleUsersInfoService = getMultipleUsersInfoService;
            this.getUserInfoService = getUserInfoService;
            this.getUserFriendsService = getUserFriendsService;
            this.getUsersService = getUsersService;
            this.getUserMatchesService = getUserMatchesService;
            this.checkIfHasTeamService = checkIfHasTeamService;
        }


        /// GET /api/users
        /// <summary>
        /// Gets multiple user's info by their Steam IDs.
        /// </summary>
        [HttpGet]
        public async Task<OkObjectResult> GetMultipleUsersInfo([FromQuery] string[] steamIds)
        {
            var request = new GetMultipleUsersInfoRequest { SteamIds = steamIds };
            return Ok(await getMultipleUsersInfoService.Execute(request));
        }

        /// GET /api/users/{{steamid}}
        /// <summary>
        /// Gets all of the user's info by its Steam ID.
        /// </summary>
        [HttpGet("{steamId}")]
        public async Task<OkObjectResult> GetUserInfo([FromRoute] string steamId)
        {
            var request = new GetUserInfoRequest { SteamId = steamId };
            return Ok(await getUserInfoService.Execute(request));
        }

        /// GET /api/users/{{steamid}}/friends
        /// <summary>
        /// Gets all of the user's friends by its Steam ID.
        /// </summary>
        [HttpGet("{steamId}/friends")]
        public async Task<OkObjectResult> GetUserFriends([FromRoute] string steamId)
        {
            var request = new GetUserFriendsRequest { SteamId = steamId };
            return Ok(await getUserFriendsService.Execute(request));
        }

        /// GET /api/users/{{steamid}}/team
        /// <summary>
        /// Checks if a user has a DreamLeague team by its Steam ID.
        /// </summary>
        [HttpGet("{steamId}/team")]
        public async Task<OkObjectResult> CheckIfHasTeam([FromRoute] string steamId)
        {
            var request = new CheckIfHasTeamRequest { SteamId = steamId };
            return Ok(await checkIfHasTeamService.Execute(request));
        }

        /// GET /api/users/{{steamid}}/matches
        /// <summary>
        /// Gets all of the user's matches played this month.
        /// </summary>
        [HttpGet("{steamId}/matches")]
        public async Task<OkObjectResult> GetUserMatches([FromRoute] string steamId)
        {
            var request = new GetUserMatchesRequest { SteamId = steamId };
            return Ok(await getUserMatchesService.Execute(request));
        }

        /// GET /api/users/all
        /// <summary>
        /// Gets all of the users in DreamLeague.
        /// </summary>
        [HttpGet("all")]
        public async Task<OkObjectResult> GetUsers([FromQuery] string? name = null, [FromQuery] RanksEnum? rank = RanksEnum.Unranked, [FromQuery] bool? isFriend = false, [FromHeader] string? steamId = null, [FromQuery] Guid? teamId = null)
        {
            var request = new GetUsersRequest { Name = name, Rank = rank, IsFriend = isFriend, SteamId = steamId, TeamId = teamId };
            return Ok(await getUsersService.Execute(request));
        }
    }
}
