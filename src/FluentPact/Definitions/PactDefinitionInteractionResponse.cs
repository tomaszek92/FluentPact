using System.Net;

namespace FluentPact.Definitions;

internal class PactDefinitionInteractionResponse
{
    public IDictionary<string, IEnumerable<string>> Headers { get; set; } = new Dictionary<string, IEnumerable<string>>();
    public HttpStatusCode Status { get; set; }
    public object? Body { get; set; }
}
