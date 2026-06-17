namespace Inventra.WebUI.Dtos.SupplierDtos
{
    public class SupplierAiAnalysisDto
    {
        public int Score { get; set; }

        public string RiskLevel { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public List<string> Strengths { get; set; } = [];

        public List<string> Weaknesses { get; set; } = [];

        public string Recommendation { get; set; } = string.Empty;
    }
}
