using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PerformanceController : ControllerBase
{
    private readonly IPerformanceService _performanceService;

    public PerformanceController(IPerformanceService performanceService)
    {
        _performanceService = performanceService;
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<PerformanceDTO>>> GetById([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _performanceService.GetPerformance(id));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<PagedResponse<PerformanceDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var performances = await _performanceService.GetPerformances(pagination);

        return performances;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] PerformanceAddDTO performance)
    {
        return this.FromServiceResponse(await _performanceService.AddPerformance(performance));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] PerformanceUpdateDTO performance)
    {
        return this.FromServiceResponse(await _performanceService.UpdatePerformance(performance));
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _performanceService.DeletePerformance(id));
    }
}