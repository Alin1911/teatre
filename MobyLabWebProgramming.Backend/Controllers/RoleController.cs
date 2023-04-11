using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers
{
    public class RoleController : AuthorizedController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService, IUserService userService) : base(userService)
        {
            _roleService = roleService;
        }
    }
}
