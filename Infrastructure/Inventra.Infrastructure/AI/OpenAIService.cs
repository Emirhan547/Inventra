using Inventra.Application.Abstractions.Infrastructures.AI;
using Microsoft.Extensions.Options;
using OpenAI.Chat;

namespace Inventra.Infrastructure.AI;

public sealed class OpenAIService
    : IAIService
{
    private readonly OpenAIOptions _options;

    public OpenAIService(
        IOptions<OpenAIOptions> options)
    {
        _options = options.Value;
    }

    public async Task<string> GenerateAsync(
        string prompt,
        CancellationToken cancellationToken = default)
    {
        var client =
            new ChatClient(
                model: "gpt-5-mini",
                apiKey: _options.ApiKey);

        var messages =
            new List<ChatMessage>
            {
                new UserChatMessage(prompt)
            };

        ChatCompletion completion =
            await client.CompleteChatAsync(
                messages,
                cancellationToken: cancellationToken);

        var result =
            completion.Content
                .FirstOrDefault()?
                .Text ?? string.Empty;

        result = result
            .Replace("```json", string.Empty)
            .Replace("```", string.Empty)
            .Trim();

        return result;
    }
}