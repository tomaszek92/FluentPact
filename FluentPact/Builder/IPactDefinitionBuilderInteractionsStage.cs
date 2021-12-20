using FluentPact.Definitions;

namespace FluentPact.Builder;

public interface IPactDefinitionBuilderInteractionsStage
{
    IPactDefinitionBuilderPublishStage WithInteractions(IEnumerable<PactDefinitionInteraction> interactions);
}
