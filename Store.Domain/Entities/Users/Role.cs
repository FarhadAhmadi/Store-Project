namespace Store.Domain.Entities.Users
{
    public class Role
    {
        public int Id { get; set; }
        public string name { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set; }
    }
}
