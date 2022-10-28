﻿using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Aplication.Services.Users.Queries.GetRoles
{
    public interface IGetRolesService
    {
        public ResultDto<List<RolesDto>> Execute();
    }
}