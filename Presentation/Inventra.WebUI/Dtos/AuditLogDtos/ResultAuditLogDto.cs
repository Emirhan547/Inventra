namespace Inventra.WebUI.Dtos.AuditLogDtos
{
    public sealed class ResultAuditLogDto
    {
        public Guid Id { get; set; }

        public string EventName { get; set; }

        public string UserName { get; set; }

        public string Description { get; set; }

        public DateTime OccurredOn { get; set; }
    }
}
