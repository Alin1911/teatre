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
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<RoleDTO>>> GetById([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _roleService.GetRole(id));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<PagedResponse<RoleDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var roles = await _roleService.GetRoles(pagination);

        return roles;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] RoleAddDTO role)
    {
        return this.FromServiceResponse(await _roleService.AddRole(role));
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] RoleUpdateDTO role)
    {
        return this.FromServiceResponse(await _roleService.UpdateRole(role));
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _roleService.DeleteRole(id));
    }
}