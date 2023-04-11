using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Implementations;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ActorController : AuthorizedController
    {
        private readonly IActorService _actorService;

        public ActorController(IUserService userService, IActorService actorService) : base(userService)
        {
            _actorService = actorService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<RequestResponse<List<Actor>>>> GetActors([FromQuery] PaginationSearchQueryParams pagination)
        {
            var actors = await _actorService.GetAllActorsAsync(pagination);
            return new RequestResponse<List<Actor>>(actors);
        }
    }
}
