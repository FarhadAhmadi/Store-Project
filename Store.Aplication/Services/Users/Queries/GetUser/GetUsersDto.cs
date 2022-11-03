namespace Store.Aplication.Services.Users.Queries.GetUser
{
    public class GetUsersDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime? RemoveTime { get; set; }
        public bool IsActive { get; set; }
    }
}
