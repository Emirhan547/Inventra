namespace Inventra.WebUI.Dtos.ProfileDtos
{
    public class ResultProfileDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FullName =>
            $"{FirstName} {LastName}";

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = [];
    }
}
