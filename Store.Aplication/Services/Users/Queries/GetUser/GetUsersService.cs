using Store.Aplication.Interface.Contexts;
using Store.Common;

namespace Store.Aplication.Services.Users.Queries.GetUser
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IDatabaseContext _databaseContext;
        public GetUsersService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public GetUsersResultDto Execute(RequestGetUserDto _Request)
        {
            var Users = _databaseContext.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(_Request.Searchkey))
            {
                Users = Users.Where(e => e.FullName.Contains(_Request.Searchkey) && e.Email.Contains(_Request.Searchkey));
            }

            int RowsCount = 0;
            var UsersList = Users.ToPaged(_Request.Page, 20, out RowsCount).Select(e => new GetUsersDto
            {
                Id = e.Id,
                FullName = e.FullName,
                Email = e.Email,
               InsertTime = e.InsertTime,
               IsRemoved = e.IsRemoved,
               RemoveTime = e.RemoveTime,
               UpdateTime = e.UpdateTime,
               IsActive = e.IsActive,
            }).ToList();

            return new GetUsersResultDto
            {
                RowsCount = RowsCount,
                Users = UsersList
            };
        }
    }
}
