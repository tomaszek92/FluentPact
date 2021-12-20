namespace FluentPact.Builder.Interaction.Request;

public interface IPactDefinitionInteractionRequestBuilder
{
    IPactDefinitionInteractionRequestBuilder WithMethod(HttpMethod method);
    IPactDefinitionInteractionRequestBuilder WithPath(string path);
    IPactDefinitionInteractionRequestBuilder WithBody(object body);
    IPactDefinitionInteractionRequestBuilder WithHeader(string key, string value);
}
