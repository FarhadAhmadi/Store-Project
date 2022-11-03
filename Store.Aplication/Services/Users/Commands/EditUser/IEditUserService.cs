using Store.Aplication.Interface.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Aplication.Services.Users.Commands.EditUser
{
    public interface IEditUserService
    {
        ResultDto Execute(RequestEditUserDto request);
    }
    public class EditUserService : IEditUserService
    {
        private readonly IDatabaseContext _context;
        public EditUserService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditUserDto request)
        {
            User? result = _context.Users.Find(request.UserId);

            if (result == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد"
                };
            }

            result.FullName = request.FullName;
            _context.SaveChanges();


            return new ResultDto()
            {
                IsSuccess = true,
                Message = "ویرایش کاربر انجام شد"
            };
        }
    }
    public class RequestEditUserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
    }
}
