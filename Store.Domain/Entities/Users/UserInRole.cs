namespace Store.Domain.Entities.Users
{
    public class UserInRole
    {
        public int Id { get; set; }


        public virtual User User { get; set; }
        public int UserId { get; set; }


        public virtual Role Role { get; set; }
        public int RoleId { get; set; }

    }
}
