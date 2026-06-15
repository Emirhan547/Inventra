namespace Inventra.WebUI.Dtos.NotificationDtos
{
    public sealed class ResultNotificationDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        public string Message { get; set; } = default!;

        public string Type { get; set; } = default!;

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
