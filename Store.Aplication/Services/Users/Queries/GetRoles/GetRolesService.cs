using Store.Aplication.Interface.Contexts;
using Store.Common.Dto;

namespace Store.Aplication.Services.Users.Queries.GetRoles
{
    public class GetRolesService : IGetRolesService
    {
        private readonly IDatabaseContext _Context;
        public GetRolesService(IDatabaseContext context)
        {
            _Context = context;
        }

        public ResultDto<List<RolesDto>> Execute()
        {
            var Roles = _Context.Roles.Select(e => new RolesDto
            {
                Id = e.Id,
                Name = e.name,
            }).ToList();

            return new ResultDto<List<RolesDto>>
            {
                Data = Roles,
                IsSuccess = true,
                Messege = ""
            };
        }
    }
}
