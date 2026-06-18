namespace Inventra.WebUI.Dtos.HealthDtos
{
    public sealed class HealthResponseDto
    {
        public string Status { get; set; } = default!;

        public List<HealthCheckDto> Checks { get; set; }
            = [];
    }
}
