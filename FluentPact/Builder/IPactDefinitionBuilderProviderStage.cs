namespace FluentPact.Builder;

public interface IPactDefinitionBuilderProviderStage
{
    IPactDefinitionBuilderInteractionsStage WithProvider(string provider);
}
