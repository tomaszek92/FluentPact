using System.Text.Json.Serialization;

namespace FluentPact.Definitions;

internal class PactDefinitionInteraction
{
    [JsonPropertyName("provider_state")]
    public string State { get; set; }
    public string Description { get; set; }
    public PactDefinitionInteractionRequest Request { get; set; }
    public PactDefinitionInteractionResponse Response { get; set; }
}
