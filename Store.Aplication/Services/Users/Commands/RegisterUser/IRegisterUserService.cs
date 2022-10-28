using Store.Aplication.Interface.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Store.Aplication.Services.Users.Commands.RegisterUser.RegisterUserService;

namespace Store.Aplication.Services.Users.Commands.RegisterUser
{
    public interface IRegisterUserService
    {
        public ResultDto<ResultRegisterUserDto> Execute(RequestRegisterUserDto request);
    }

    public class RegisterUserService : IRegisterUserService
    {
        private readonly IDatabaseContext _Context;
        public RegisterUserService(IDatabaseContext context)
        {
            _Context = context;
        }

        public ResultDto<ResultRegisterUserDto> Execute(RequestRegisterUserDto request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email))
                {
                    return new ResultDto<ResultRegisterUserDto>
                    {
                        Data = new ResultRegisterUserDto
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Messege = "پست الکترونیک خود را وارد کنید "
                    };
                }

                if (string.IsNullOrEmpty(request.FullName))
                {

                    return new ResultDto<ResultRegisterUserDto>
                    {
                        Data = new ResultRegisterUserDto
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Messege = "نام خود را وارد کنید"
                    };



                }
                if (string.IsNullOrEmpty(request.passWord))
                {

                    return new ResultDto<ResultRegisterUserDto>
                    {
                        Data = new ResultRegisterUserDto
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Messege = "رمز عبور را وارد کنید"
                    };
                }
                if (request.passWord != request.RePassword)
                {

                    return new ResultDto<ResultRegisterUserDto>
                    {
                        Data = new ResultRegisterUserDto
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Messege = "رمز ورود و تکرار آن برابر نیست"
                    };
                }

                User user = new User
                {
                    Email = request.Email,
                    FullName = request.FullName,

                };
                List<UserInRole> userInRoles = new List<UserInRole>();
                foreach (var item in request.Roles)
                {
                    var Role = _Context.Roles.Find(item.Id);
                    userInRoles.Add(new UserInRole
                    {
                        Role = Role,
                        RoleId = Role.Id,
                        User = user,
                        UserId = user.Id
                    });
                    user.UserInRoles = userInRoles;

                    _Context.Users.Add(user);
                    _Context.SaveChanges();
                }
                return new ResultDto<ResultRegisterUserDto>()
                {
                    Data = new ResultRegisterUserDto()
                    {
                        UserId = user.Id,
                    },
                    IsSuccess = true,
                    Messege = "ثبت نام با موفقیت انجام شد"
                };
            }
            catch (Exception)
            {
                return new ResultDto<ResultRegisterUserDto>
                {
                    Data = new ResultRegisterUserDto
                    {
                        UserId = 0
                    },
                    IsSuccess = false,
                    Messege = "ثبت نام انجام نشد"
                };
            }
        }
        public class RequestRegisterUserDto
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string passWord { get; set; }
            public string RePassword { get; set; }
            public List<RolesRegisterUserDto> Roles { get; set; }


        }
        public class RolesRegisterUserDto
        {
            public int Id { get; set; }

        }
        public class ResultRegisterUserDto
        {
            public int UserId { get; set; }
        }
    }
}
