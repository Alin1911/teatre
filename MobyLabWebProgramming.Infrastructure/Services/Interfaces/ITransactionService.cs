using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ITransactionService
{
    Task<ServiceResponse<TransactionDTO>> GetTransaction(Guid id, CancellationToken cancellationToken = default);
    Task<ServiceResponse<PagedResponse<TransactionDTO>>> GetTransactions(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    Task<ServiceResponse> AddTransaction(TransactionAddDTO transaction, CancellationToken cancellationToken = default);
    Task<ServiceResponse> UpdateTransaction(TransactionUpdateDTO transaction, CancellationToken cancellationToken = default);
    Task<ServiceResponse> DeleteTransaction(Guid id, CancellationToken cancellationToken = default);
}