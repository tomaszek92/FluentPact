using System.Text.Json.Serialization;

namespace FluentPact.Definitions;

public class PactDefinitionInteraction
{
    [JsonPropertyName("provider_state")]
    public string State { get; init; }
    public string Description { get; init; }
    public PactDefinitionInteractionRequest Request { get; init; }
    public PactDefinitionInteractionResponse Response { get; init; }
}
