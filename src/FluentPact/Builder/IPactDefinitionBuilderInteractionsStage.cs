using FluentPact.Builder.Interaction;

namespace FluentPact.Builder;

public interface IPactDefinitionBuilderInteractionsStage
{
    IPactDefinitionBuilderInteractionsStage WithInteraction(Action<IPactDefinitionInteractionBuilderGivenStage> interaction);    
    IPactDefinitionBuilderFinalStage PublishAsFile(string path);
}
