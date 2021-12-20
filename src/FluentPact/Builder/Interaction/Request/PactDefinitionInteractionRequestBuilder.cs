using FluentPact.Definitions;

namespace FluentPact.Builder.Interaction.Request;

internal class PactDefinitionInteractionRequestBuilder : IPactDefinitionInteractionRequestBuilder
{
    private readonly PactDefinitionInteractionRequest _request = new();

    public IPactDefinitionInteractionRequestBuilder WithMethod(HttpMethod method)
    {
        if (method == HttpMethod.Get || method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Delete)
        {
            _request.Method = method.Method;
        }
        else
        {
            throw new ArgumentNullException(nameof(method), $"Unsupported method: {method.Method}");
        }
        return this;
    }

    public IPactDefinitionInteractionRequestBuilder WithPath(string path)
    {
        _request.Path = path;
        return this;
    }

    public IPactDefinitionInteractionRequestBuilder WithBody(object body)
    {
        _request.Body = body;
        return this;
    }

    public IPactDefinitionInteractionRequestBuilder WithHeader(string key, string value)
    {
        _request.Headers.Add(key, value);
        return this;
    }

    public PactDefinitionInteractionRequest Build() => _request;
}
