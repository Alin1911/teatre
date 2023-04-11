using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers
{
    public class HallController : AuthorizedController
    {
        private readonly IHallService _hallService;

        public HallController(IHallService hallService, IUserService userService) : base(userService)
        {
            _hallService = hallService;
        }
    }
}
