using System.Net;
using FluentPact.Definitions;

namespace FluentPact.Builder.Interaction.Response;

internal class PactDefinitionInteractionResponseBuilder : IPactDefinitionInteractionResponseBuilder
{
    private readonly PactDefinitionInteractionResponse _response = new();

    public IPactDefinitionInteractionResponseBuilder WithStatus(HttpStatusCode httpStatusCode)
    {
        _response.Status = httpStatusCode;
        return this;
    }

    public IPactDefinitionInteractionResponseBuilder WithHeader(string key, string value)
    {
        _response.Headers.Add(key, value);
        return this;
    }

    public IPactDefinitionInteractionResponseBuilder WithBody(object body)
    {
        _response.Body = body;
        return this;
    }

    public PactDefinitionInteractionResponse Build() => _response;
}
