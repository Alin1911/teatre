using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;


namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces
{
    public interface IPieceService
    {
        Task<ServiceResponse<PieceDTO>> GetPiece(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResponse<PagedResponse<PieceDTO>>> GetPieces(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
        Task<ServiceResponse> AddPiece(PieceAddDTO piece, CancellationToken cancellationToken = default);
        Task<ServiceResponse> UpdatePiece(PieceUpdateDTO piece, CancellationToken cancellationToken = default);
        Task<ServiceResponse> DeletePiece(Guid id, CancellationToken cancellationToken = default);
    }
}
