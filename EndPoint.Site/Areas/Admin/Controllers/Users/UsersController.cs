using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using Store.Aplication.Services.Users.Commands.RegisterUser;
using Store.Aplication.Services.Users.Queries.GetRoles;
using Store.Aplication.Services.Users.Queries.GetUser;

namespace EndPoint.Site.Areas.Admin.Controllers.Users
{
    [Area("admin")]
    public class UsersController : Controller
    {
        private readonly IGetUsersService _GetUsersService;
        private readonly IGetRolesService _GetRolesService;
        private readonly IRegisterUserService _RegisterUserService;
        public UsersController(IGetUsersService getUsersService, IGetRolesService getRolesService, IRegisterUserService registerUserService)
        {
            _GetUsersService = getUsersService;
            _GetRolesService = getRolesService;
            _RegisterUserService = registerUserService;
        }
        [HttpGet]
        public IActionResult Index(string Searchkey, int Page)
        {
            return View(_GetUsersService.Execute(new RequestGetUserDto
            {
                Searchkey = Searchkey,
                Page = Page
            }));
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_GetRolesService.Execute().Data, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(string Email, string FullName, int RoleId, string Password, string RePassword)
        {
            var result = _RegisterUserService.Execute(new RequestRegisterUserDto
            {
                FullName = FullName,
                Email = Email,
                Roles = new List<RolesRegisterUserDto>()
                {
                    new RolesRegisterUserDto
                    {
                        Id = RoleId,
                    }
                },
                passWord = Password,
                RePassword = RePassword
            });

            return Json(result);
        }
    }
}