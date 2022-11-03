using Store.Aplication.Interface.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Users;
using Store.Common;
using Bugeto_Store.Common;

namespace Store.Aplication.Services.Users.Commands.RegisterUser
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IDatabaseContext _Context;
        public RegisterUserService(IDatabaseContext context)
        {
            _Context = context;
        }

        public ResultDto<ResultRegisterUserServiceDTO> Execute(RequestRegisterUserServiceDTO request)
        {
            if (string.IsNullOrEmpty(request.FullName))
            {
                return new ResultDto<ResultRegisterUserServiceDTO>()
                {
                    Data = new ResultRegisterUserServiceDTO
                    {
                        UserId = 0,
                    },
                    IsSuccess = false,
                    Message = "نام خود را وارد کنید"
                };
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                return new ResultDto<ResultRegisterUserServiceDTO>()
                {
                    Data = new ResultRegisterUserServiceDTO
                    {
                        UserId = 0,
                    },
                    IsSuccess = false,
                    Message = "ایمیل خود را وارد کنید"
                };
            }
            if (string.IsNullOrEmpty(request.Password))
            {
                return new ResultDto<ResultRegisterUserServiceDTO>()
                {
                    Data = new ResultRegisterUserServiceDTO
                    {
                        UserId = 0,
                    },
                    IsSuccess = false,
                    Message = "رمز عبور خود را وارد کنید"
                };
            }
            if (string.IsNullOrEmpty(request.RePassword))
            {
                return new ResultDto<ResultRegisterUserServiceDTO>()
                {
                    Data = new ResultRegisterUserServiceDTO
                    {
                        UserId = 0,
                    },
                    IsSuccess = false,
                    Message = "تکرار رمز عبور اشتباه است"
                };
            }

            PasswordHasher? passwordHasher = new PasswordHasher();
            var hashedPassword = passwordHasher.HashPassword(request.Password);

            User user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = hashedPassword,
                IsActive = true,
              
            };

            List<UserInRole> UserInRoles = new List<UserInRole>();
            foreach (RoleDTO? item in request.Roles)
            {
                Role? roles = _Context.Roles.Find(item.Id);

                UserInRoles.Add(new UserInRole
                {
                    Role = roles,
                    RoleId = item.Id,
                    User = user,
                    UserId = user.Id
                });

            }
            user.UserInRoles = UserInRoles;

            _Context.Users.Add(user);
            _Context.SaveChanges();

            return new ResultDto<ResultRegisterUserServiceDTO>()
            {
                Data = new ResultRegisterUserServiceDTO
                {
                    UserId = user.Id,
                },
                IsSuccess = true,
                Message = "saved"
            };
        }
    }
}
