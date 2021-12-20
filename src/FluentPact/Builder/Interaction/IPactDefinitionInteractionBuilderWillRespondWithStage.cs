using FluentPact.Builder.Interaction.Response;

namespace FluentPact.Builder.Interaction;

public interface IPactDefinitionInteractionBuilderWillRespondWithStage
{
    void WillRespondWith(Action<IPactDefinitionInteractionResponseBuilder> buildResponse);
}
