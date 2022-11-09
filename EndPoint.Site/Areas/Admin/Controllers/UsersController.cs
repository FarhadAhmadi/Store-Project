using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using Store.Aplication.Services.Users.Commands.ChangeUserStatus;
using Store.Aplication.Services.Users.Commands.EditUser;
using Store.Aplication.Services.Users.Commands.RegisterUser;
using Store.Aplication.Services.Users.Commands.RemoveUser;
using Store.Aplication.Services.Users.Queries.GetRoles;
using Store.Aplication.Services.Users.Queries.GetUser;
using Store.Common.Dto;

namespace EndPoint.Site.Areas.Admin.Controllers.Users
{
    [Area("admin")]
    public class UsersController : Controller
    {
        private readonly IGetUsersService _GetUsersService;
        private readonly IGetRolesService _GetRolesService;
        private readonly IRegisterUserService _RegisterUserService;
        private readonly IRemoveUserService _RemoveUserService;
        private readonly IChangeUserStatusService _ChangeUserStatusService;
        private readonly IEditUserService _EditUserService;
        public UsersController(IGetUsersService getUsersService, IGetRolesService getRolesService, IRegisterUserService registerUserService,
            IRemoveUserService removeUserService, IChangeUserStatusService changeUserStatusService, IEditUserService editUserService)
        {
            _GetUsersService = getUsersService;
            _GetRolesService = getRolesService;
            _RegisterUserService = registerUserService;
            _RemoveUserService = removeUserService;
            _ChangeUserStatusService = changeUserStatusService;
            _EditUserService = editUserService;
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
        public IActionResult Create(string fullName , string email , int roleId , string password , string rePassword )
        {
            ResultDto<ResultRegisterUserServiceDTO> result =  _RegisterUserService.Execute(new RequestRegisterUserServiceDTO
            {
                FullName = fullName,
                Email = email,
                Roles = new List<RoleDTO>()
                {
                    new RoleDTO
                    {
                        Id = roleId
                    }
                },
                Password = password,
                RePassword = rePassword
            });

            return Json(result);
        }
        [HttpPost]
        public IActionResult Remove(int userId)
        {
            return Json(_RemoveUserService.Execute(userId));
        }
        [HttpPost]
        public IActionResult ChangeUserStatus(int userId)
        {
           return Json(_ChangeUserStatusService.Execute(userId));
        }
        public IActionResult Edit(int userId, string fullName)
        {
            return Json(_EditUserService.Execute(new RequestEditUserDto
            {
                FullName = fullName,
                UserId = userId
            }));
        }
    }
}