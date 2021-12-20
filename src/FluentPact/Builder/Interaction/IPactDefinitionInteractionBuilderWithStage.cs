using FluentPact.Builder.Interaction.Request;

namespace FluentPact.Builder.Interaction;

public interface IPactDefinitionInteractionBuilderWithStage
{
    IPactDefinitionInteractionBuilderWillRespondWithStage With(Action<IPactDefinitionInteractionRequestBuilder> buildRequest);
}
