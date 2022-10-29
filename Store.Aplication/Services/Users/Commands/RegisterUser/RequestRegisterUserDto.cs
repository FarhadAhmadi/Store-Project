namespace Store.Aplication.Services.Users.Commands.RegisterUser
{
    public class RequestRegisterUserDto
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string passWord { get; set; }
            public string RePassword { get; set; }
            public List<RolesRegisterUserDto> Roles { get; set; }


        }
    }

