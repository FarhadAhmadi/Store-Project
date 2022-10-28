using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Aplication.Services.Users.Queries.GetUser
{
    public interface IGetUsersService
    {
        GetUsersResultDto Execute(RequestGetUserDto _Request);
    }
}
