using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Infrastructures.AI
{
    public interface IAIService
    {
        Task<string> GenerateAsync(string prompt,CancellationToken cancellationToken = default);
    }
}
