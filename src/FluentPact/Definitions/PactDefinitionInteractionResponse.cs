using System.Net;

namespace FluentPact.Definitions;

public class PactDefinitionInteractionResponse
{
    public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public HttpStatusCode Status { get; set; }
    public object? Body { get; set; }
}
