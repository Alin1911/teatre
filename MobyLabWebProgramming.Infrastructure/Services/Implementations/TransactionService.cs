using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;

        public TransactionService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<TransactionDTO>> GetTransaction(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _repository.GetAsync(new TransactionProjectionSpec(id), cancellationToken);

            return result != null ?
                ServiceResponse<TransactionDTO>.ForSuccess(result) :
                ServiceResponse<TransactionDTO>.FromError(CommonErrors.TransactionNotFound);
        }

        public async Task<ServiceResponse<PagedResponse<TransactionDTO>>> GetTransactions(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
        {
            var result = await _repository.PageAsync(pagination, new TransactionProjectionSpec(pagination.Search), cancellationToken);

            return ServiceResponse<PagedResponse<TransactionDTO>>.ForSuccess(result);
        }

        public async Task<ServiceResponse> AddTransaction(TransactionAddDTO transaction, CancellationToken cancellationToken = default)
        {
            var newTransaction = new Transaction
            {
                TotalPrice = transaction.TotalPrice,
                Status = transaction.Status,
                UserId = transaction.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(newTransaction, cancellationToken);

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> UpdateTransaction(TransactionUpdateDTO transaction, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(new TransactionSpec(transaction.Id), cancellationToken);

            if (entity != null)
            {
                entity.TotalPrice = transaction.TotalPrice ?? entity.TotalPrice;
                entity.Status = transaction.Status ?? entity.Status;
                entity.UserId = transaction.UserId ?? entity.UserId;
                entity.UpdatedAt = DateTime.UtcNow;

                await _repository.UpdateAsync(entity, cancellationToken);
            }

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> DeleteTransaction(Guid id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync<Transaction>(id, cancellationToken);

            return ServiceResponse.ForSuccess();
        }
    }
}