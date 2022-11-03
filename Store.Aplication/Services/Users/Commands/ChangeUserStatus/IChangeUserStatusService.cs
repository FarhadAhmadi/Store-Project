using FluentAssertions.Execution;
using Store.Aplication.Interface.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Aplication.Services.Users.Commands.ChangeUserStatus
{
    public interface IChangeUserStatusService
    {
        ResultDto Execute(int UserId);
    }
    public class ChangeUserStatusService : IChangeUserStatusService
    {
        private readonly IDatabaseContext _Context;
        public ChangeUserStatusService(IDatabaseContext context)
        {
            _Context = context;
        }
        public ResultDto Execute(int UserId)
        {
            if (UserId == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد"
                };
            }

            User? result = _Context.Users.Find(UserId);
            if (result.IsActive == true)
            {
                result.IsActive = false;
            }
            else
            {
                result.IsActive = true;
            }

            _Context.SaveChanges();

            string userStatus = result.IsActive == true ? "فعال " : "غیر فعال";

            return new ResultDto
            {
                IsSuccess = true,
                Message = $"کاربر با موفقیت {userStatus} شد"
            };
        }
    }
}