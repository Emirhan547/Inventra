namespace Inventra.WebUI.Dtos.UserDtos
{
    public sealed class UserDetailDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
