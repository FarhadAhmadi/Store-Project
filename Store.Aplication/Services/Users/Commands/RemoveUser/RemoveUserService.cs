using Store.Aplication.Interface.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Users;

namespace Store.Aplication.Services.Users.Commands.RemoveUser
{
    public class RemoveUserService : IRemoveUserService
    {
        private readonly IDatabaseContext _Context;
        public RemoveUserService( IDatabaseContext databaseContext)
        {
                _Context = databaseContext;
        }
        public ResultDto Execute(int UserId)
        {
            User? User = _Context.Users.Find(UserId);

            if (User == null)
            {
                new ResultDto
                {
                    IsSuccess = false,
                    Message = " کاربر یافت نشد"
                };
            }

            User.RemoveTime = DateTime.Now;
            User.IsRemoved = true;

            _Context.SaveChanges();

            return new ResultDto 
            {
                IsSuccess = true,
                Message = "کاربر با موفقیت حذف شد"
            };
        }
    }
}