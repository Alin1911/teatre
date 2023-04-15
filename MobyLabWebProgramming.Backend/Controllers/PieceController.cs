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
public class PieceController : ControllerBase
{
    private readonly IPieceService _pieceService;

    public PieceController(IPieceService pieceService)
    {
        _pieceService = pieceService;
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<PieceDTO>>> GetById([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _pieceService.GetPiece(id));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<PagedResponse<PieceDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var pieces = await _pieceService.GetPieces(pagination);

        return pieces;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] PieceAddDTO piece)
    {
        return this.FromServiceResponse(await _pieceService.AddPiece(piece));
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] PieceUpdateDTO piece)
    {
        return this.FromServiceResponse(await _pieceService.UpdatePiece(piece));
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _pieceService.DeletePiece(id));
    }
}