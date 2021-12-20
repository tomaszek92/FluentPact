namespace FluentPact.Builder;

public interface IPactDefinitionBuilderOptionsStage
{
    IPactDefinitionBuilderConsumerStage WithOptions(bool ignoreCasing, bool ignoreContractValues);
}
