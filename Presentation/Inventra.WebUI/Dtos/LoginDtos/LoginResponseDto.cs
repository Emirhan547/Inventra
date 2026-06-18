namespace Inventra.WebUI.Dtos.LoginDtos
{
    public sealed class LoginResponseDto
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpirationTime { get; set; }
        public List<string> Roles { get; set; } = [];
    }
}
