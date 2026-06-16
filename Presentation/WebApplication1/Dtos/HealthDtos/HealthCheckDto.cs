namespace Inventra.WebUI.Dtos.HealthDtos
{
    public sealed class HealthCheckDto
    {
        public string Name { get; set; } = default!;

        public string Status { get; set; } = default!;

        public string? Description { get; set; }
    }
}
