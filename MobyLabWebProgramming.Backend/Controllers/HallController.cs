using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Implementations;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class HallController : ControllerBase
{
    private readonly IHallService _hallService;

    public HallController(IHallService hallService)
    {
        _hallService = hallService;
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<HallDTO>>> GetById([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _hallService.GetHall(id));
 
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<PagedResponse<HallDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var halls  = await _hallService.GetHalls(pagination);

        return halls;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] HallAddDTO hall)
    {
        return this.FromServiceResponse(await _hallService.AddHall(hall));
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] HallUpdateDTO hall)
    {
        return this.FromServiceResponse(await _hallService.UpdateHall(hall));
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _hallService.DeleteHall(id));
    }
}