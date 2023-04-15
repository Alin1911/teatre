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
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<TicketDTO>>> GetById([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _ticketService.GetTicket(id));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<PagedResponse<TicketDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var tickets = await _ticketService.GetTickets(pagination);

        return tickets;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] TicketAddDTO ticket)
    {
        return this.FromServiceResponse(await _ticketService.AddTicket(ticket));
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] TicketUpdateDTO ticket)
    {
        return this.FromServiceResponse(await _ticketService.UpdateTicket(ticket));
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _ticketService.DeleteTicket(id));
    }
}