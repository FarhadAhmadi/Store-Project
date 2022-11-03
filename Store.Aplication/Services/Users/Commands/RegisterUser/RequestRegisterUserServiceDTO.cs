namespace Store.Aplication.Services.Users.Commands.RegisterUser
{
    public class RequestRegisterUserServiceDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<RoleDTO> Roles { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
