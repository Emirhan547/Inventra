namespace Inventra.WebUI.Dtos.AuditLogDtos
{
    public sealed class AuditLogFilterDto
    {
        public string? EventName { get; set; }

        public string? UserName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
