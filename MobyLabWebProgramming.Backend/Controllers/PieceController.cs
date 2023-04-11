using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers
{
    public class PieceController : AuthorizedController
    {
        private readonly IPieceService _pieceService;

        public PieceController(IPieceService pieceService, IUserService userService) : base(userService)
        {
            _pieceService = pieceService;
        }

    }
}
