using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Suppliers.Results
{
    public sealed class GetSupplierAiAnalysisResponse
    {
        public int Score { get; set; }

        public string RiskLevel { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public List<string> Strengths { get; set; } = [];

        public List<string> Weaknesses { get; set; } = [];

        public string Recommendation { get; set; } = string.Empty;
    }
}
