namespace Inventra.WebUI.Dtos.UserDtos
{
    public sealed class UserFilterDto
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }
    }
}
