namespace FluentPact.Definitions;

public class PactDefinitionInteractionRequest
{
    public string Method { get; init; }
    public string Path { get; init; }
    public object? Body { get; init; }
    public IDictionary<string, string> Headers { get; init; } = new Dictionary<string, string>();
}
