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
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RequestResponse<ActorDTO>>> GetById([FromRoute] Guid id)
        {
            return this.FromServiceResponse(await _actorService.GetActor(id));

        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<PagedResponse<ActorDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
        {
            return await _actorService.GetActors(pagination);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RequestResponse>> Add([FromBody] ActorAddDTO actor)
        {
            return this.FromServiceResponse(await _actorService.AddActor(actor));
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<RequestResponse>> Update([FromBody] ActorUpdateDTO actor)
        {
            return this.FromServiceResponse(await _actorService.UpdateActor(actor));
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
        {
            return this.FromServiceResponse(await _actorService.DeleteActor(id));
        }
    }
}
