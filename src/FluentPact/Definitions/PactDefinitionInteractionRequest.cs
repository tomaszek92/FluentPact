namespace FluentPact.Definitions;

public class PactDefinitionInteractionRequest
{
    public string Method { get; set; }
    public string Path { get; set; }
    public object? Body { get; set; }
    public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
}
