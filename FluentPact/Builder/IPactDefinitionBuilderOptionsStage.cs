using FluentPact.Definitions;

namespace FluentPact.Builder;

public interface IPactDefinitionBuilderOptionsStage
{
    IPactDefinitionBuilderConsumerStage WithOptions(PactDefinitionOptions options);
}
