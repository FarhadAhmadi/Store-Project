namespace Store.Aplication.Services.Users.Queries.GetUser
{
    public class GetUsersResultDto
    {
        public List<GetUsersDto> Users { get; set; }
        public int RowsCount { get; set; }
    }
}
