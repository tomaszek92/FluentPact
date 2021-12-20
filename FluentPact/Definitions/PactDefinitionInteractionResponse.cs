using System.Net;

namespace FluentPact.Definitions;

public class PactDefinitionInteractionResponse
{
    public IDictionary<string, string> Headers { get; init; } = new Dictionary<string, string>();
    public HttpStatusCode Status { get; init; }
    public object Body { get; init; }
}
