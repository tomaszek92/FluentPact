using System.Net;

namespace FluentPact.Builder.Interaction.Response;

public interface IPactDefinitionInteractionResponseBuilder
{
    IPactDefinitionInteractionResponseBuilder WithStatus(HttpStatusCode httpStatusCode);
    IPactDefinitionInteractionResponseBuilder WithHeader(string key, string value);
    IPactDefinitionInteractionResponseBuilder WithBody(object body);
}
