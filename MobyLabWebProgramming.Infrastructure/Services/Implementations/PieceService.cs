using System;
using System.Threading;
using System.Threading.Tasks;
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
    public class PieceService : IPieceService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;

        public PieceService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<PieceDTO>> GetPiece(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _repository.GetAsync(new PieceProjectionSpec(id), cancellationToken);

            return result != null ?
                ServiceResponse<PieceDTO>.ForSuccess(result) :
                ServiceResponse<PieceDTO>.FromError(CommonErrors.PieceNotFound);
        }

        public async Task<ServiceResponse<PagedResponse<PieceDTO>>> GetPieces(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
        {
            var result = await _repository.PageAsync(pagination, new PieceProjectionSpec(pagination.Search), cancellationToken);

            return ServiceResponse<PagedResponse<PieceDTO>>.ForSuccess(result);
        }

        public async Task<ServiceResponse> AddPiece(PieceAddDTO piece, CancellationToken cancellationToken = default)
        {
            var newPiece = new Piece
            {
                Titlu = piece.Titlu,
                Autor = piece.Autor,
                Descriere = piece.Descriere,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(newPiece, cancellationToken);

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> UpdatePiece(PieceUpdateDTO piece, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(new PieceSpec(piece.Id), cancellationToken);

            if (entity != null)
            {
                entity.Titlu = piece.Titlu ?? entity.Titlu;
                entity.Descriere = piece.Descriere ?? entity.Descriere;
                entity.Autor = piece.Autor ?? entity.Autor;
                entity.UpdatedAt = DateTime.UtcNow;

                await _repository.UpdateAsync(entity, cancellationToken);
            }

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> DeletePiece(Guid id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync<Piece>(id, cancellationToken);

            return ServiceResponse.ForSuccess();
        }
    }
}