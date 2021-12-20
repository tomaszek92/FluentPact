using FluentPact.Builder.Interaction;

namespace FluentPact.Builder;

public interface IPactDefinitionBuilderInteractionsStage
{
    IPactDefinitionBuilderInteractionsStage WithInteraction(Action<IPactDefinitionInteractionBuilder> interaction);    
    IPactDefinitionBuilderFinalStage PublishAsFile(string path);
}
