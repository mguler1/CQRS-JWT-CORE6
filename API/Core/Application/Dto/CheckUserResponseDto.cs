namespace API.Core.Application.Dto
{
    public class CheckUserResponseDto
    {
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public int RoleId { get; set; }
        public bool IsExist { get; set; }
    }
}
