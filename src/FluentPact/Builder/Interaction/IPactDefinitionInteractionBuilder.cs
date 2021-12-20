using FluentPact.Builder.Interaction.Request;
using FluentPact.Builder.Interaction.Response;

namespace FluentPact.Builder.Interaction;

public interface IPactDefinitionInteractionBuilder
{
    IPactDefinitionInteractionUponReceivingStage Given(string state);
}

public interface IPactDefinitionInteractionUponReceivingStage
{
    IPactDefinitionInteractionWithStage UponReceiving(string description);
}

public interface IPactDefinitionInteractionWithStage
{
    IPactDefinitionInteractionWillRespondWithStage With(Action<IPactDefinitionInteractionRequestBuilder> buildRequest);
}

public interface IPactDefinitionInteractionWillRespondWithStage
{
    void WillRespondWith(Action<IPactDefinitionInteractionResponseBuilder> buildResponse);
}
