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
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<TransactionDTO>>> GetById([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _transactionService.GetTransaction(id));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<PagedResponse<TransactionDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var transactions = await _transactionService.GetTransactions(pagination);

        return transactions;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] TransactionAddDTO transaction)
    {
        return this.FromServiceResponse(await _transactionService.AddTransaction(transaction));
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] TransactionUpdateDTO transaction)
    {
        return this.FromServiceResponse(await _transactionService.UpdateTransaction(transaction));
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _transactionService.DeleteTransaction(id));
    }
}